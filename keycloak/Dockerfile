FROM quay.io/keycloak/keycloak:26.6.1

# pre-build the optimised server config (runs at build time, not startup)
RUN /opt/keycloak/bin/kc.sh build

ENTRYPOINT ["/opt/keycloak/bin/kc.sh", "start"]