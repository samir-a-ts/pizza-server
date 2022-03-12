FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
ENV MongoDBPass="wye8W4cYin4ZeAZt"
ENV JwtSecret="JnininNIUn9usjniWMDIOEANUFI9i0om0oifn9oun0oiwnf9--[]][P]lP[K90jd0kP=SLLCVO;;-K0EKV90WML-=;"

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["PizzaAPI.csproj", "./"]
RUN dotnet restore "PizzaAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PizzaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /
COPY --from=public /public/ /app/public/
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaAPI.dll"]
