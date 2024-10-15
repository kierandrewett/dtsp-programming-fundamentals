#!/usr/bin/env bash

PROJ_PATH="$1"
PROJ_TYPE="$2"

mkdir -p $PROJ_PATH
cd $PROJ_PATH
dotnet new $PROJ_TYPE
