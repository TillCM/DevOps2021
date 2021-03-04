# Jenkins Deployment


## Initial Server Configuration

The included playbook idompotently applies server configuration to ensure the **Jenkins** server is correctly configured.

The destination server (Virtual Machine) is specified in the `inventory/dev` or `inventory/hosts` inventory files for dev and production environments respectively.

This playbook can be tested against a clean local Ubuntu 20.04 LTS VM install with ssh-server installed and a single user `vagrant` with password `vagrant`.

## Reliance on docker registry

The deploy playbook assumes that the images required are available publicly or in the docker registry.  Images are built and pushed to the cyber-mint registry as follows:

```
docker build -t cybermint/teamfu-ui:latest .
docker tag cybermint/teamfu-ui:latest registry.digitalocean.com/cybermint/teamfu-ui:latest
doctl auth init --access-token $DIGITALOCEAN_API_KEY
doctl registry login
docker push registry.digitalocean.com/cybermint/teamfu-ui:latest
```

## Local VM Deployment

To prepare your development environment you will need to do the following:

1. Create a virtual machine based on an Ubuntu 20.04 LTS server image with ssh-server installed.  Optionally create a user `vagrant` with password `vagrant` during the installation, or the script assumes the user `root`.

2. Install ansible in your dev environment land prep your environment as follows
```
sudo apt install ansible
# make sure that your inventory/dev and inventory/prod point to the appropriate host/IP addresses respectively eg. `10.0.0.123` or `teamfu.tech`
# From the playbook folder test your installation with
ansible -i inventory/dev -m ping jenkins --user vagrant --ask-pass
```
This should ask for your password and reward you with:
```
SSH password: 
192.168.122.162 | SUCCESS => {
    "ansible_facts": {
        "discovered_interpreter_python": "/usr/bin/python3"
    },
    "changed": false,
    "ping": "pong"
}
```

or `ansible -i inventory/dev -m setup jenkins --user vagrant` for everything you ever wanted to know about the teamfu vmserver!

> NOTE: my VM happened to be on local ip : 192.168.122.162

To idompotently run this against the `jenkins` you will need to copy your public key to the `.ssh` folder in this repo.  This is safe to store in SCM but NEVER copy your private key.

From within the `playbook` folder:
```
cp ~/.ssh/<name-of-key>.pub .ssh/<name-of-key>.pub
```

## Running the Playbook locally

The default user is `vagrant` , so if you provisioned your ubuntu 20.04LTS VM with a different user or wish to use a different user then first `export VM_USER=<user>`. 

When you run the playbook for the first time you need to enter the `root` or `vagrant` password to ssh & for sudo access.
You may also need to ensure you have a set `export DIGITALOCEAN_API_KEY=<DO-API-KEY>` with Read Only credentials if pulling from `cybermint` registry.
This can be done here `https://cloud.digitalocean.com/account/api/tokens` 
You can add this line to end of your `~/bashrc` and `source ~/.bashrc` to reload these configs in the shell.

```
ANSIBLE_NOCOWS=1 ansible-playbook deploy.yml -i inventory/dev -u vagrant --ask-pass --ask-become-pass --extra-vars "digital_ocean_api_key=$DIGITALOCEAN_API_KEY"
```
Subsequent playbook runs wont need these anymore as it will use your ssh key to connect and a passwordless sudo access.

```
ANSIBLE_NOCOWS=1 ansible-playbook deploy.yml -i inventory/dev -u <user> --extra-vars "digital_ocean_api_key=$DIGITALOCEAN_API_KEY"
```
If you have multiple SSH keys then you can add the `--key-file` and specify the path to your SSH key.

Keep in mind password access will no longer be possible - so DO NOT run this script without your public key in the `public_keys` folder or you will lock yourself out of the server.

In order to pull images from the private Digital Ocean registry, you must include a command line parameter with your DigitalOcean API key. 
```bash
ANSIBLE_NOCOWS=1 ansible-playbook deploy.yml -i inventory/dev -u <user> --extra-vars "digital_ocean_api_key=<your digital ocean API key>" 
```

When running to a local VM the Lets Encrypt step will fail (this is to be expected as there is no FQDN mapped to the VM).  The deployment should work in all other respects.


## Running Playbook on Production

On the assumption that a new VM in digital ocean has been provisioned.  The default cloud user is root, and login will be via ssh key.

```
# we only run this once with root user , there-after we use vagrant
ANSIBLE_NOCOWS=1 ansible-playbook deploy.yml -i inventory/prod -u root --private-key ~/.ssh/id_rsa --extra-vars "digital_ocean_api_key=$DIGITALOCEAN_API_KEY"
```


---
&copy; Copyright 2021, Cyber-Mint (Pty) Ltd.     
[www.cyber-mint.com](https://www.cyber-mint.com)

