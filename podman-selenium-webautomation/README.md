# Podman Selenium Webautomation

Proof of Concept of how we can leverage selenium to scrape pages within a container

## Prerequisites
- Linux based distro (I used CentOS 8)
- Podman/Docker
- Python 3.8 and selenium installed (`python3 -m pip install selenium`)

## Quick start

1) Run container `docker run -d -p 4444:4444 --shm-size="2g" selenium/standalone-chrome:4.3.0-20220726`
1) Run the following python snippet:

```python
from selenium import webdriver
from selenium.webdriver.common.desired_capabilities import DesiredCapabilities
import time

print("Opening Chrome...")

website = "https://www.google.com/"

driver = webdriver.Remote("http://127.0.0.1:4444/wd/hub", DesiredCapabilities.CHROME)

print(f"Going to Checkout {website}, checkout http://127.0.0.1:4444/ui#")

try:
    driver.get("https://www.google.com/")

    print("Sleeping for 2 minutes, then quitting...")
    time.sleep(120)

    driver.quit()

except Exception as e:
    print(e.message)

print("Done!")
```

## Known issues

It's currently a challenge to reconnect to an existing chrome session, so my advice for now is to stop the pod and rerun it. There is probably a better way - but it's a POC. 

## References

- [https://github.com/SeleniumHQ/docker-selenium](https://github.com/SeleniumHQ/docker-selenium) 
