#!/usr/bin/env bash

export xOS="linux"
if [[ $OSTYPE == darwin* ]]; then
    export xOS="macos"
elif [[ $OSTYPE == msys ]] || [[ $OSTYPE == cygwin ]]; then
    export xOS="windows"
fi

MISC_DIR=`readlink -f "$BASH_SOURCE" | xargs dirname`
ROOT_DIR="$MISC_DIR/tools/$xOS"
export PATH="$ROOT_DIR":"$ROOT_DIR/protobuf/bin":"$PATH"
