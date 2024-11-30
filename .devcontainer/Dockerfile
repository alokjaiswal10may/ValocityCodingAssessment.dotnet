# https://learn.microsoft.com/en-us/dotnet/core/docker/introduction#net-images
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.18 as development

ARG USERNAME=vscode
ENV USERNAME $USERNAME

# To check for updates run
# doas apk -U upgrade --no-cache --simulate 
RUN apk -U upgrade --no-cache \
    # Create the user
    && adduser "$USERNAME" -G wheel --disabled-password \
    # Run apk info pkgName to get the version number of a package
    # If any issues accure remove all version pinning, rebuild, then use the apkinfo cmd to get the latest version
    && apk add --no-cache --update \
    # Add zip to compress and decompress files
    zip=3.0-r12 \
    # Add doas support. So one can experiment in the development stage
    # https://wiki.archlinux.org/title/Doas
    doas=6.8.2-r4 \
    # Install angular dependencies
    nodejs=18.18.2-r0 \
    npm=9.6.6-r0 \
    && echo "permit nopass :wheel as root" >> /etc/doas.d/doas.conf \
    # Install docker file linter hadolint https://github.com/hadolint/hadolint
    && wget -q -O /bin/hadolint https://github.com/hadolint/hadolint/releases/download/v2.12.0/hadolint-Linux-x86_64 \
    && chmod +x /bin/hadolint \
    # Update dotnet workloads
    && dotnet workload update

USER $USERNAME
WORKDIR /workspace