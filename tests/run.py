from invoker import Invoker
from models.route import Route

class POC:
    def __init__(self):
        self.servers = {
            "europe": "eu",
            "north america": "na",
            "asia pacific": "ap",
            "korea": "kr",
            "latina america": "latam",
            "brazil": "br"
        }
        self.host: str = f"https://{self.servers.get('europe')}.api.riotgames.com"
        self.invoker: Invoker = Invoker()
        self.routes: list[Route] = [
            Route("GET", "matchlists", "val/match/v1/matchlists/by-puuid"),
            Route("GET", "content", "val/content/v1/contents"),
            Route("GET", "status", "val/status/v1/platform-data")
        ]
        self.api_key: str = "XXX"
        self.puuid: str = "N7-FsuzPwVNz02bQpO23lkadF4rV70ZttZj3JgXgaNMe0YsnOhUBhD4zeuy0Rqhmbg-X9mXWZeJ1jg"
        self.tests = [
            self.__account__(),
            self.__find__()
        ]

        self.__run__()

    def __run__(self):
        for test in self.tests:
            route, url = test

            r = self.invoker.invoke(
                method=route.method,
                url=url,
                params={"api_key": self.api_key},
                headers={},
                data={},
                auth={}
            )

            print(f"{r.status_code}: {r.url}")

            if (r.status_code >= 200 and r.status_code <= 299):
                with open(f"{route.name}.json", "w+") as f:
                    f.write(r.text)

    def __account__(self):
        route = self.__find_route__("content")
        url = "{}".format(
            self.__prepare__(route)
        )

        return (route, url)

    def __find__(self):
        route = self.__find_route__("matchlists")
        url = "{}{}".format(
            self.__prepare__(route),
            self.puuid
        )

        return (route, url)

    def __prepare__(self, route: Route):
        return (f"{self.host}/{route.entrypoint}")

    def __find_route__(self, name):
        for route in self.routes:
            if (route.name == name):
                return (route)
        return (None)

if (__name__ == "__main__"):
    POC()
