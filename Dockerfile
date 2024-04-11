FROM postgres:alpine

ADD hotelmanagement.sql /docker-entrypoint-initdb.d

RUN chmod a+r /docker-entrypoint-initdb.d/*
