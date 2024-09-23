VERSION                       =  "2024.09.05"
AUTHOR                        =  "EpicMorg"
MODIFIED                      =  "STAM"
DOCKER_SCAN_SUGGEST           =  false
PIP_BREAK_SYSTEM_PACKAGES     =  1

app:
	@make -s version
	@make -s help

version:
	@echo "=================================================="
	@echo " Jira Quick Issue Creator, version: ${VERSION}, [` git branch --show-current `]"
	@echo "=================================================="

help:
	@echo "make help                         - show this help."
	@echo "make version                      - show version of this repository."
	@echo "make pip                          - intall kaniko-wrapper and requirements."
	@echo "make git                          - git add . ; git commit ; git push"
	@echo "make build                        - legacy option. just shows kaniko-wrapper version."
	@echo "make dry-run                      - dry build with kaniko-wrapper. without deploy."
	@echo "make test                         - same as make dry-run"
	@echo "make dry                          - same as make dry-run"
	@echo "make build-compose                - build with docker-compose engine."
	@echo "make deploy                       - build and deploy with kaniko-wrapper."
	@echo "make deploy-compose               - deploy with docker-compose engine."
	@echo "make clean                        - cleanup docker."

git:
	git add .
	git commit -am "make - autocommit"
	git push

pip:
	@echo "======================================="
	@echo "===== Installing kaniko-wrapper   ====="
	@echo "======================================="
	-rm -rfv /usr/lib/python3.6/EXTERNALLY-MANAGED || true
	-rm -rfv /usr/lib/python3.7/EXTERNALLY-MANAGED || true
	-rm -rfv /usr/lib/python3.8/EXTERNALLY-MANAGED || true
	-rm -rfv /usr/lib/python3.9/EXTERNALLY-MANAGED || true
	-rm -rfv /usr/lib/python3.10/EXTERNALLY-MANAGED || true
	-rm -rfv /usr/lib/python3.11/EXTERNALLY-MANAGED || true
	-rm -rfv /usr/lib/python3.12/EXTERNALLY-MANAGED || true
	-rm -rfv /usr/lib/python3.13/EXTERNALLY-MANAGED || true
	-rm -rfv /usr/lib/python3.14/EXTERNALLY-MANAGED || true
	-pip3 install --break-system-packages -r requirements.txt || true
	-pip install --break-system-packages -r requirements.txt || true

build:
	kaniko-wrapper --version

dry:
	make dry-run

test:
	make dry-run

dry-run:
	kaniko-wrapper --dry-run

build-compose:
	docker-compose build --compress --parallel --progress plain

deploy:
	kaniko-wrapper --deploy

deploy-compose:
	docker-compose push

clean:
	docker container prune -f
	docker image prune -f
	docker network prune -f
	docker volume prune -f
	docker system prune -af
