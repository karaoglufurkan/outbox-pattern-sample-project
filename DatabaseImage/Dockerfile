FROM mcr.microsoft.com/mssql/server:2019-CU13-ubuntu-20.04

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=PassWord1234
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433

WORKDIR /src
COPY DatabaseImage/Scripts.sql .

RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" &&  /opt/mssql-tools/bin/sqlcmd -S127.0.0.1 -Usa -PPassWord1234 -i Scripts.sql