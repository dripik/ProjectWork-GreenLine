Per esporre porta di redis al ip del host e renderlo accessibile anche al difuori del container
sudo docker run --rm -p 6379:6379 -h xxxx -d redis
comando per collegarsi a docker usando la redis-cli dalla bash di linux e non dal container docker
sudo redis-cli -h 127.0.0.1 -p 6379

docker exec -it XXXXX sh per collegarsi da console docker al posto delle xxxx va il nome del container che è visibile dal comando 
docker container list 