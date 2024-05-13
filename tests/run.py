from invoker import Invoker
from models.route import Route

class POC:
    def __init__(self):
        self.servers = {
            "europe": "europe",
            "america": "america"
        }
        self.host: str = f"https://{self.servers.get('europe')}.api.riotgames.com"
        self.invoker: Invoker = Invoker()
        self.routes: list[Route] = [
            Route("GET", "matchlists", "/val/match/v1/matchlists/by-puuid")
        ]
        self.api_key: str = "XXX"
        self.puuid: str = "N7-FsuzPwVNz02bQpO23lkadF4rV70ZttZj3JgXgaNMe0YsnOhUBhD4zeuy0Rqhmbg-X9mXWZeJ1jg"

        self.__run__()

    def __run__(self):
        route = self.__find_route__("matchlists")
        url = "{}{}".format(
            self.__prepare__(route),
            self.puuid
        )
        r = self.invoker.invoke(method=route.method, url=url, params={"api_key": self.api_key}, headers={}, data={}, auth={})

        print(f"{r.status_code}: {r.text}")

    def __prepare__(self, route: Route):
        return (f"{self.host}/{route.entrypoint}")

    def __find_route__(self, name):
        for route in self.routes:
            if (route.name == name):
                return (route)
        return (None)

if (__name__ == "__main__"):
    POC()
