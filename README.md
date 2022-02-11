# KeyLogger

Note: *I, Jacob Glik, do not approbate malicious use of this program, for any reason.*
**WARNING: KeyLogging is a criminal offence**. It is **ILLIGAL**! **Do not** use or run this code on anyone elses machine unless granded explicit permision.

Summary: Get the victims keystrokes 

Note: The getPassword() and getEmail() function definitions have been removed from this code to keep the email, password, and encrypted counterparts secret.

Steps
1. Get keystrokes from operating system
2. Save keystrokes to textfile
3. Send file as email to attacker periodicly and restart the cycle
   - Using an email proxy account that auto forwards emails to the correct address to distance the attacker from the keylogger
     - The distance can be further imrpoved by encrypting the email as the proxy and forwarding it to 25,000 random emails including the attackers email who can decrypt the email
5. Information Obstruction
   - Run in the bacground as a process
   - Rename to a random common windows process
   - Mask as that windows process
   - Mask data files as system.dll files
   - Transfer information through email when internet use is already high so that the sending of information will be lost in the clutter
