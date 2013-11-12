#Usage

- Set the ```ES_URL``` environment variable. E.g. ```http://es.domain.com:9200```
- Import the scheduled task into windows using Task Scheduler.  I have exported the task xml as ```elasticsearch-light.xml```.  
This runs for me as the built-in Windows user NETWORK SERVICE. 
- This project can also be built on Mono and entered into crontab

#Dependencies

- [qJake/SharpHue](https://github.com/qJake/SharpHue)
- [bbyars/buildlight](https://github.com/bbyars/buildlight)
