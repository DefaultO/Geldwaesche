# Geldwäsche
This project wouldn't be possible without https://github.com/appsec-labs/Advanced_Packet_Editor. I coded ontop of this already open source project. What this does is exploiting some state of the [game](https://gangster.goodgamestudios.com/) the [Devs](https://www.goodgamestudios.com/) of it overlooked. When you claim something, the server doesn't yet remember it. Apparantly it only starts to save state-related stuff on the server, when you disconnect.

So by causing a force disconnect by the game server, and not giving the client any time to upload state-related things, you bypass the claim you did and can repeat it. More to it, just by logging into another Account using a login packet, it seem like to bypass the claim upload already, by it, possibly updating the wrong account. This project does a force disconnect though, by loging into the same account at least twice. It's the simplest form of this exploit and I am sure, there are other "force disconnect" methods. This won't work until the eternity and the server starts to figure it out after a lot of claims (if you are lucky).

What makes this so good is that there are no visible signs besides the Achievments that could tell that you did exploit this [game](https://gangster.goodgamestudios.com/). This makes the exploits based on this overlook - ghost features. Means there are no server-sided logs that tell your account is abnormal, neither is it obvious and you probably won't get banned using them (only manuals, and those probably won't happen).

Because this Game stopped getting updates since 2017, [officially stated by the Game Studio](https://goodgamestudios.com/de/blog/warum-goodgame-gangster-shadow-kings-co-keine-updates-erhalten/2016/07/05/), this exploit will stay undetected and work for the rest of it's existence. They literally don't touch the game anymore, just keep it running, because people keep paying into it.

With the [base](https://github.com/appsec-labs/Advanced_Packet_Editor) opens up a new Window, with checkboxes. These checkboxes have labels next to them that relate to game functions/features. If you checked the checkbox, whenever you do what you normally would do in Missions, for example, you get disconnected. And upon reconnecting you will be greeted, most of the times, with the fact, that it didn't get claimed.

![image](https://user-images.githubusercontent.com/42414542/122101039-29bf4200-ce14-11eb-81ef-c76ddab2dd16.png)
## Features
- Auto Attach & -Reattach. If the Advanced Packet Editor is unattached to a process it picks the first process that process name is "Goodgame Gangster", looking for that process name happens once every second using a timer. The Packet Editor detects itself if the program got closed. There I set the attached bool back to false and the timer repeats the (re)attaching.
- Server Detection based on packet structure. This works by splitting the whole packet by "%", and checking what stands on the second index. Then picking the right account out of a string array that gets assigned properly on the Main Forms Load function/method.
- Loading trash accounts to log-into out of ``Accounts.cfg``, a File that gets created upon first application launch. Means once this project got compiled, and you don't seek for changes, you never ever have to compile it again.
- Force Disconnect upon fullfilling all checks. Checkbox checked, packet data contains some for the game's feature unique string and packet data contains some form of "MafiaEx" ("startduel" for example wouldn't be enough alone to identify the target packet, a packet that contains it gets sent twice).
## For 300IQ's/Developers/potentioal Forkers
I put a comment saying "Geldwäsche" next to most things I edited or added ontop of the [base](https://github.com/appsec-labs/Advanced_Packet_Editor). This got never sold, but had a loader at some point as an anti-leak measure. All of that got removed, so I hadn't to include the projects I used using project references. The Core of this project is in ``Main.cs``, and the exploit / trigger packet detection happens at the bottom of the ``UpdateMainGrid(byte[] data)`` function:
```csharp
// Geldwäsche
string dataAsString = ae.GetString(data);
if (dataAsString.Contains("MafiaEx"))
{
    // Some unecessary looking if condition that prevents doing the assigning and the loop everytime if it doesn't meet. Will get optimized anyways while compiling, if there is a better way.
    if (dataAsString.Contains("stopworking") || dataAsString.Contains("quitmission") || dataAsString.Contains("startwantedfight") || dataAsString.Contains("startgangwar") || dataAsString.Contains("startduel"))
    {
        string forceDisconnectPacket = "";
        string server = dataAsString.Split('%')[2];
        foreach (string account in Geldwäsche.Accounts)
        {
            if (account.Contains(server))
            {
                forceDisconnectPacket = account;
                forceDisconnectPacket = Regex.Unescape(forceDisconnectPacket); // Very important!
            }
        }
        if (!string.IsNullOrEmpty(forceDisconnectPacket))
        {
            if (Geldwäsche.WorkToggled && dataAsString.Contains("stopworking"))
            {
                ForceDisconnect(forceDisconnectPacket: forceDisconnectPacket);
            }
            if (Geldwäsche.MissionToggled && dataAsString.Contains("quitmission"))
            {
                ForceDisconnect(forceDisconnectPacket: forceDisconnectPacket);
            }
            if (Geldwäsche.WantedToggled && dataAsString.Contains("startwantedfight"))
            {
                ForceDisconnect(forceDisconnectPacket: forceDisconnectPacket);
            }
            if (Geldwäsche.GangwarsToggled && dataAsString.Contains("startgangwar"))
            {
                ForceDisconnect(forceDisconnectPacket: forceDisconnectPacket);
            }
            if (Geldwäsche.DuelToggled && dataAsString.Contains("startduel"))
            {
                ForceDisconnect(forceDisconnectPacket: forceDisconnectPacket);
            }
        }
    }
}
```
