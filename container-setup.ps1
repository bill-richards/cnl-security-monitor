[CmdletBinding()]Param(
    [Parameter(Mandatory=$true,Position=0)][String]$PersistenceRoot,
    [Parameter(Mandatory=$false)][String]$User = 'cnl_user',
    [Parameter(Mandatory=$false)][String]$Pword = 'cnl_Password1',
    [Parameter(Mandatory=$false)][int]$LocalHttpPort = 15672
)

cls

$rabbit_data_dir = Join-Path $PersistenceRoot 'rabbit_data'

# create the persistence directories if they do not exist
if(!(Test-Path $rabbit_data_dir))
{
      New-Item -Path $rabbit_data_dir -Type "directory" -Force
}

# Create a Network to run all images on
docker network create cnl-demo

# run the Rabbitmq server container linked to the persistence directory
docker run -it -d --name rabbitmq-server -v ${rabbit_data_dir}:/bitnami --network cnl-demo `
  -p ${LocalHttpPort}:15672 -e RABBITMQ_USERNAME=${User} -e RABBITMQ_PASSWORD=${Pword} `
  -p 5672:5672 `
  bitnami/rabbitmq:latest

docker ps

