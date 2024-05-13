class Route:
    def __init__(self, method: str, name: str, entrypoint: str):
        self.name: str = name
        self.method: str = method
        self.entrypoint: str = entrypoint
