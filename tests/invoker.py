import requests

class Invoker:
    def __init__(self):
        pass

    def invoke(self, method, url: str, params: dict, headers: dict, data: dict, auth: dict):
        request = requests.Request(
            method,
            url = url,
            params = params,
            headers = headers,
            data = data,
            auth = auth
        )

        prepared = request.prepare()
        response = requests.Session().send(prepared)

        return (response)
