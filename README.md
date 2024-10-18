# KeyLogger

Note: *I, Jacob Glik, do not approbate malicious use of this program for any reason.*
**WARNING: KeyLogging is a criminal offense**. It is **ILLIGAL**! **Do not** use or run this code on anyone else's machine unless granted explicit permission.

Summary: Get the victim's keystrokes 

Note: The `getPassword()` and `getEmail()` function definitions have been removed from this code to keep the email, password, and encrypted counterparts secret.

Steps
1. Get keystrokes from the operating system
2. Save keystrokes to textfile
3. Send the file as an email to the attacker periodically and restart the cycle
   - Using an email proxy account that auto-forwards emails to the correct address to distance the attacker from the keylogger
     - The distance can be further improved by encrypting the email as the proxy and forwarding it to 25,000 random emails, including the attacker's email who can decrypt the email
5. Information Obstruction
   - Run in the background as a process
   - Rename to a random common Windows process
   - Mask as that Windows process
   - Mask data files as system.dll files
   - Transfer information through email when internet use is already high so that the sending of information will be lost in the clutter
