#!/usr/bin/env bash

[[ "$TRACE" ]] && set -x
OWD=`pwd`
pushd `dirname "$0"` > /dev/null
trap __EXIT EXIT

colorful=false
tput setaf 7 > /dev/null 2>&1
if [[ $? -eq 0 ]]; then
    colorful=true
fi

function __EXIT() {
    popd > /dev/null
}

function printError() {
    $colorful && tput setaf 1
    >&2 echo "Error: $@"
    $colorful && tput setaf 7
}

function printImportantMessage() {
    $colorful && tput setaf 3
    >&2 echo "$@"
    $colorful && tput setaf 7
}

function printUsage() {
    $colorful && tput setaf 3
    >&2 echo "$@"
    $colorful && tput setaf 7
}

source ../var.sh

printImportantMessage "====== $xOS ======"

mkdir -p "$xOS"

function installTool() {
    echo "Installing $1..."
    GOBIN=`cd $xOS && pwd` go install "$2"
}

installTool "archivist" github.com/kingsgroupos/archivist/cli/archivist
installTool "easyjson" github.com/mailru/easyjson/...
installTool "errcheck" github.com/kisielk/errcheck
installTool "hotswap" github.com/edwingeng/hotswap/cli/hotswap
installTool "protoc-gen-gogo" github.com/gogo/protobuf/protoc-gen-gogo
