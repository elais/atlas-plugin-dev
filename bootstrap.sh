#!/usr/bin/env bash

# Add extra repositories
apt-key adv --keyserver keyserver.ubuntu.com --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/debian wheezy main" | tee /etc/apt/sources.list.d/mono-xamarin.list

apt-get update

# Install Mono
apt-get install -y mono-complete

# Install node and other nice things
apt-get -y install nodejs-legacy npm
npm install -g yo grunt-cli generator-aspnet bower

# Install libuv
apt-get install -y -f automake libtool curl
curl -sSL https://github.com/libuv/libuv/archive/v1.4.2.tar.gz | tar zxfv - -C /usr/local/src
cd /usr/local/src/libuv-1.4.2 && sh autogen.sh
cd /usr/local/src/libuv-1.4.2 && ./configure
cd /usr/local/src/libuv-1.4.2 && make
cd /usr/local/src/libuv-1.4.2 && make install
rm -rf /usr/local/src/libuv-1.4.2
cd ~/ && ldconfig

# atlas-run-connect () {
#     if [ "$1" = "--product" ]
#        if [ "$2" = "jira" ] ; then
#            atlas-run-standalone --product jira --bundled-plugins com.atlassian.bundles:json-schema-validator-atlassian-bundle:1.0.4,com.atlassian.webhooks:atlassian-webhooks-plugin:2.0.0,com.atlassian.jwt:jwt-plugin:1.2.2,com.atlassian.upm:atlassian-universal-plugin-manager-plugin:2.19.1.2-D20150723T232127,com.atlassian.plugins:atlassian-connect-plugin:1.1.40 --jvmargs -Datlassian.upm.on.demand=true
#        elif [ "$2" = "confluence" ] ; then
#            atlas-run-standalone --product confluence --bundled-plugins com.atlassian.bundles:json-schema-validator-atlassian-bundle:1.0.4,com.atlassian.webhooks:atlassian-webhooks-plugin:1.0.6,com.atlassian.jwt:jwt-plugin:1.2.2,com.atlassian.upm:atlassian-universal-plugin-manager-plugin:2.19.1.2-D20150723T232127,com.atlassian.plugins:atlassian-connect-plugin:1.1.40 --jvmargs -Datlassian.upm.on.demand=true
#        fi
#     fi
# }
