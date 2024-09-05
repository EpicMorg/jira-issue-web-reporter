all: jwr

jwr:
	docker build --compress -t epicmorg/jira-issue-web-reporter .
	docker push epicmorg/jira-issue-web-reporter
