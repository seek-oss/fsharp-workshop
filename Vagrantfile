# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure(2) do |config|
  config.vm.box = "janihur/ubuntu-1404-desktop"
  #config.vm.network "forwarded_port", guest: 80, host: 8080
  # config.vm.synced_folder "../data", "/vagrant_data"
  config.vm.provider "virtualbox" do |vb|
    # Display the VirtualBox GUI when booting the machine
    vb.gui = true
    # Customize the amount of memory on the VM:
    vb.memory = "1024"
  end
 
  config.vm.provision "shell", inline: <<-SHELL
    apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
    echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list
    apt-get update -y
    apt-get upgrade -y
    apt-get install -f -y
    apt-get install git -y
    apt-get install mono-devel -y
    apt-get install monodevelop -y
    apt-get install terminator -y
    apt-get install gvfs-bin -y
    wget -q https://github.com/atom/atom/releases/download/v1.0.2/atom-amd64.deb -O /tmp/atom-amd64.deb
    dpkg -i /tmp/atom-amd64.deb
    apm install atom-fsharp -y
    SHELL
  end
