# SCPSL-Vip-Plugin


This plugin is for automatically giving out VIP advantages 


## Setup

Open EXILED config

```bash
nano 7777-config.yml
```

Edit VIP roles under VIP plugin (1 means that VIP can use the advantage just once a day. If you want to disable the advantage just put 0.) example: 

```bash
  - role_name: CHAD
    class_spawn: 1
    mtf_chaos: 2
    blackout: 0
```

Edit database path to your directory example:
```bash
  /home/users/peanut
```
## Commands

Spawn as a specific [class](Classes.md) (With default config VIP can use this 15 seconds after start of an round.)
```bash
.spawn <class name>
```
Force spawn wave (MTF/CHAOS) (VIP can use this the entire round)
```bash
.sw <wave name>
```
Blackout (With default config VIP can use this 15 seconds after start of an round.)
```bash
.blackout
```