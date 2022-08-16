FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443
USER root

RUN apt-get update && apt-get upgrade -y \
    && apt install -y zsh curl git wget \
    && usermod -s $(which zsh) root \
    && zsh --version

ENV TZ 'Asia/Tehran'

RUN echo $TZ > /etc/timezone && \
    apt-get update && apt-get install -y tzdata && \
    rm /etc/localtime && \
    ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata && \
    apt-get clean

#######################
## Copy Dependencies ##
#######################

COPY "API/API.csproj" "API/"
COPY "Application/Application.csproj" "Application/"
COPY "Domain/Domain.csproj" "Domain/"
COPY "infrastructure/infrastructure.csproj" "infrastructure/"

#######################
## Restore Packages  ##
#######################
RUN dotnet restore "API/API.csproj"

COPY . ./

#######################
## Build Project     ##
#######################
WORKDIR /app/API
RUN dotnet build "API.csproj" -c Release -o /app/build

#######################
## Publish Project   ##
#######################
FROM build-env as publish-env
RUN dotnet publish "API.csproj" -c Release -o /app/publish

#######################
## Run Project       ##
#######################
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim as final-env
WORKDIR /app
COPY --from=publish-env /app/publish /app
ENTRYPOINT [ "dotnet", "API.dll" ]



