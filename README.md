Drop Table Team:
Dave Pannu
Alberto Valdiviez
Alan Paul Paragas

Working Title: Death Dungeon
Introduction
In Death Dungeon, the main objective of our game is to create a turn-based mobile game full of adventure and excitement. This game will be a single player game for the Universal Windows Platform iOS and Android platform. The game is relatively simple and not processor intensive, therefore will work with older versions of both iOS and android. 
The game’s purpose is to provide the user an exciting turn based game with a dungeon crawling theme. It will allow you to play the objective of going throughout a merciless dungeon to see how far you adventure till death comes for you. Additionally, users are able to simulate dungeon runs if they choose. Go the furthest and put your best score on the leaderboard of best runs.
As a dungeon crawling adventurer, you will start with six powerful characters with unique classes to build your group. Your goal from that point is to defeat unique and fearsome monsters on the way, collecting loot items and experience to further level your character development. While death is imminent for your adventures, your legacy will be added to Death Dungeon leaderboard. Do you have what it takes to venture the depths of Death Dungeon? 

Main Features
Platforms (UWP and Android Oreo 8.0 and iOS 11.0)
Single player
Turn Based Dungeon Crawler
Grid-Map Battlefields
Auto attack random monster or focus attack specific monster
Game Mechanics
Listings
Character, monster, and items are listed according to:
Highest level
Highest experience
Character before monster
Alphabetic on name
If equal just according to listing
Start Game
Be able to create new game runs
Create an adventure group
Generate your game run
First fight occurs after creation
Autoplay
User can simulate the entire game or battle
Battle simulation allows you to distribute items, game sim will not
Character Selection
Create a party of 6 characters
Unique Character creation
Choose a party of 6 characters from provided character classes 
Can have multiple of the same class	
Character Development
After selection characters level up by gaining experience
Have body locations to equip items
Locations include Head, Body, Feet, Left Hand, Right Hand, Left Finger, Right Finger
Each item can increase 1 attribute per slot equipped
Monsters
15 different types of monsters
Each new battle starts with 6 monsters
Monster types vary with attributes
Defeating monster experience vary according to type, level, and damage inflicted
Level of monsters will also scale according to level of characters
Monster cannot participate in battle once defeated at health 0
Items
Random attribute bonuses (increase the value of 1 attribute e.g +3 defense or +2 speed, but not both)
Items dropped by monster’s
Item value and attribute scale according to level of characters during drop
Items can only be equipped to designated location of body
Battle Engine
Turn Based Management 
Speed determines who attacks first and how far a player can move on the board before attempting an attack.  The higher the speed the more spaces a character can move. 
Initiative based on Speed, the highest speed character attacks first.  If there are multiple characters with same speed, tie is broken in the following order - highest level, highest experience points, character before monster, alphabetically by name, and first in list order.
Players can choose what monster to attack if they wish to focus fire on a specific strong monster, otherwise game will select random monster to attack if one is not focused on
Must wait till all characters and monsters have had an attack before next turn
Attack damage determined by Weapon Damage, Character Attack, and Level Damage
Defense determines the percentage of a character or monster of successfully avoiding an attack
All damage to health is updated after every successful attack
Damage inflicted on monster gives character experience once finished with attack
After a monster is defeated and if it drops and item the item will be added to a pool which can be collected and distributed from after all monsters in that round are dead and before the player continues on to the next round
Defeating all monsters in a round will end round and distribution of item is up to user and what appropriate body part to equip it 
Give it to any character
Leveling 
Damage inflicted to monster gives experience based on percent of damage
Defeating monsters gives experience
Attributes scale to level
Leveling up occurs as soon as experience is sufficient 
Can occur during battle
Dieing
Character dies once health reaches 0 
Character will remain dead for remainder of run
Exp no longer dispersed
Items drop and able to requip to other characters
 Game continues till all characters die
End Game
Once all characters die, game ends
Player’s run is saved to leaderboard
Final score will be sum of total experience gained 
Will display score, rounds turns, monsters/characters killed, and list of items



EXPANDED MECHANICS


Character Creation
	Players will be able to start the game by selecting a party of 6 characters consisting of any number of the following premade classes Warrior, Wizard, Rogue, Cleric, Ranger, and Druid.  All new characters will start with maximum health. 
Each class will start will the following base stats.

Classes	Defense _	Speed _	Attack 
Warrior 	+7	   	+3		+7
Wizard 	+3		+7		+7
Rogue   	+5		+5		+7
Cleric		+3		+7		+3
Ranger	+3		+5		+7
Druid		+5		+5		+5	

Once a character has earned enough experience to level their stats will increase, details below in leveling mechanics.


Monsters
The game will consist of the 15 following monsters. Monster will have a given name and descriptions. Each with varying starting attack, defense, and speed. These stats will be dependent and generated relative to the average level of the characters at the beginning of each battle round. The increased level and difficulty of each monster will in turn reward more experience upon damage dealt. 



Bats
    	Imp
    	Blob
   	Rabbit
   	Goblin
	
    	Bandits
    	Giant
Lich
	Stone Golem
	Wolf
	
	Dragon
	Succubus
	Ghost
	ManBearPig
	Demon


At the start of each battle the player will encounter 6 monsters. Each monster will have a chance to drop an item, which can be equipped on characters at the end of the round. The level of bonus that each item will drop will be related to the level of the characters.



Attacking
   	Character and monsters will be placed on a grid battlefield at the beginning of a round. Initiative will be based on speed will all be attributed to which monsters or character can move and attack first.  For tie breaker on initiative the game will determine by first checking highest level, then highest experience points, alphabetic on name, and finally first in list order.

 Players have two options when attacking.  They can click the fight button and let the game select the monster to attack or they can focus attack on a specific monster if they choose by tapping the monster they wish to attack before tapping fight. Each attack will consist of a roll of a 20 sided die (new seed each roll).  A successful attack will consist of 
(Roll + Level + Attack Modifiers) > (Target Defense + Target Level + Target Defense Modifiers).  
If the Defense is greater than Attack than the attacker will miss their attack

For each attack damage is based off (Weapon Damage + Base attack + Level Damage).  Level damage is calculated by ¼ of level rounded to nearest integer eg. (8*¼) = 2.  Successful attacks will reduce target health by whole numbers and health is updated.  Once a character or monster health is <=0, the character or monster dies and can no longer attack or be attacked.  Player characters cannot be revived and will remain dead for the remainder of the game.



Items
	As the game progresses characters will have the opportunity of modifying their stats by collecting items dropped by monsters. Locations for items consist of Head, Neck, Feet, Primary Hand, Offhand Hand, Left Finger, Right Finger.  The items will only be allowed to modify 1 attribute but attributes can stack and only one item per location is allowed. As player level increases the base stat for items will be increased So that level 15 characters don’t have to deal with low level items dropping.  If a player wishes to equip and item on a character location that already has one, then the removed item can be given to another character.
	If a character dies their items can be distributed among surviving party members at the end of the current battle. All items not distributed at the end of the battle are destroyed and lost. All item allocation will be done in between rounds after all monsters have been killed and before heading to next round of monsters


Experience
The experience gained from each monster will be based on the level of the monster, it’s current experience that it’s holding and the percent of damage the character did to that monster’s health pool.  Each successful hit on a monster will decrease its remaining available experience.


Leveling up
	If the experience earned for a character is greater than the experience needed to next level the character will level up.  Level checking will be done at the end of each characters successful attack and will occur during battles.  Characters health pool will not be replenished with leveling up but instead their current health will be offset by their new maximum health e.g. max health 10, current health is 8, level up to 2d10, get 15 as new max health, so new current health is 15-(10-8) or (newmax-(oldmax-current)).  There is no limit to how much a character can level up in a single battle.  If they land every attack and the rest of the party misses all their attacks that single character can end the battle at level 6 and the rest of the party continues on at level 1.  Maximum character level is capped at 20 and maximum health is capped at (20d10).  As a character levels up their base stats will increase in accordance to the chart posted below.  Stats only change if there is a difference in value between the new level and the current level.  For example going from level 1 to level 2 the character only gains 1 defense as Attack and Speed are both still 1 between the two levels.



AutoPlay
	Selecting Autoplay will result in the game running in entirety without input from the player.  The game will immediately progress from the home screen to the score screen. 

System Architecture
The icon inventory will be stored in database and connected to the game engine.
Wire Frame




Screen Name : Game Screen

Description:

This Screen is the first view of the app. It has tabs that access the score screen, character screens, monster screens, item screens,  and the about page. Then there is the name of the app and its logo. Lastly there are buttons to access the the autoplay feature, the battle sequence, and items pool.

UX Description:

This screen will be built on a tabbed page. The tabs will link to the Score, Character, Monster, Items, and About page screen.
The game name, logo, and buttons will be in a stacked layout. The name will use a label view, the logo will use an image view, and the buttons will use button views.

Interactive Description:
The score button will link to a leaderboard or score screen.The character tab will link to the character CRUDI screens. The Monster tab will link to the Monster CRUDI. The item tab will ink to the item CRUDI screens. The battle button will start a new battle sequence, where character party, and monsters are chosen before linking to the battle rounds. The inventory button will link to the pool of dropped items screen.





Screen Name: Score Screen

Description:
This screen will show the score and details of the last match played and it will show a leaderboard of the best scores recorded so far.

UX Description:
This screen will be built on a content page. Inside this content page there will be a stack layout. The text for the last attempt will be a label view. The next section will be four blocks of labels and binded data. The labels and and the data will be paired using a relative layout side by side. The text and data will be displayed using label views. Next there will be another stacked text to display the high score.
And lastly a list view of the record holders. Each item will be seen through a view cell with binded data of the name of the record holder and their score. The bottom of the stack layout is a button view that is link to the game screen.

Interactive Description: 
The user can see the data and the scores. When the finished the button can be pressed to return to the game screen.


Screen Name: Character Start Screen

Description: 
This is the first character screen. It shows the inventory of available characters. When a character is selected it will show stats and the icon of that selected character. Next to each name there is a button that is link to the character read screen, and Character delete screen. The create button links to the character update screen. The back button goes to the main screen. All characters will be available for selection in between rounds.

UX Description: 
The character start screen is built on a content page with a overall stack layout. The top of the stack will be an image view of a logo. Next on the stack will be a label view for the word characters. The following will be a list view of a data template. In the Template will be a view cell with each list item having three views paired through a relative layout placing the three items on a single line. The three items will be a binded data of the character name, a button view for the view button, and a button view for the delete button.  The bottom of the stack will to stacked button views for create and back.


Interactive Description:
The user can press the view button the read page of the corresponding character. The delete buttons will link to a delete page to confirm the removal of the character. The back button will link to the game page. The create button will link to the character create page to create a custom character.



Screen Name: Character Read Screen

Description: This is the character read screen. When any character is selected to view, this page is seen. It shows the character icon, stats, stat proficiencies, and class weapons

UX description:
The screen will be built on a content page. The screen will use a stack layout. The top of the stack will be a label view of the character name.Next will be two elements side by side in relative layout. The image at the right will be an image view of the character icon, and the left will be a listview of character stats.
Next on the stack will be another relative layout of two views side by side.
The element on the left will be a list view of class special stat modifiers and the left element will be a list view of class specific weapons.

Interactive Description: The user will mostly read the details form this screen. The first button labeled back goes to the character start screen. The button labeled edit goes to the character update or edit screen. The delete button goes to the character update screen to confirm deletion of the character. Current health and experience to next level will be displayed when character viewed in between rounds. 







Screen Name: Character Create Screen

Description: This is the new character Screen. It lists the options that can be selected to create a custom character. 

Ux Description: This screen will be built on a content page. The starting layout will be stacked layout. The top of the stack will be a label view of the screen description. The next level will be a relative layout of two views. The first view will be a label view with the text “Enter name”. The second element in the relative pair will be a text field. The next stacked element will be a scrollview view of text  Each line will be binded to data of the available charcter classes. The next stacked item will be a label view indication to select an icon.The next stacked element is a scroll view of available character icons. Next on the stack is two paired views in relative layout with stat modifiers and class weapons binded to class specified starter weapons. Lastly there are two button views in a relative layout.


Interactive Description:
The user will be able to design a character on this page. They can give the character a name in the textfield, choose a class by selecting a class. Selecting a class changes the image icons available, weapon and armor the character starts with and stat modifiers. The user can only pick the name, the class, and image icon. At the bottom, the user will have the ability to press the roll stats button for the possibility to have a higher level character. The save button will allow the user to save the details and will return the user to the character-start page. Roll stats will either increase or decrease the base character level and experience, thus changing starting stats regardless of class.

Screen Name: Character Update Screen

Description: 
This screen will allow the user to change certain features about their characters. They can change the name, the image icon, and class.

UX Description:
This screen will be built on a content page. The starting layout will be stacked layout. The top of the stack will be a label view of the screen description. After, there is a label view of the character name. The next level will be a relative layout of two views. The first view will be a label view with the text “Enter name”. The second element in the relative pair will be a text field. The next stacked element will be a scrollview of text character class. Each line will be binded to data of the available character classes. The next stacked item will be a label view indication to select an icon.The next stacked element is a scroll view of available character icons. Next on the stack is two paired views in relative layout with stat modifiers and class weapons. Lastly there is a button view.


Interactive Description:
The user can type in a new name, re-choose the characters class, and choose a character icon. Choosing a new class will change the available icons, starting weapons, and stat modifiers. The save button will save the changes and return the user to the character read screen.



Screen Name: Character Delete

Description: 
This is screen is shown when a user decided to delete a character. It shows a summary for the character and has button to either confirm or stop deletion

UX Description:
The screen will be built on a content page. The screen will use a stack layout. The top of the stack will be a label view of the character name and delete request. Next will be two elements side by side in relative layout. The Image at the right will be an image view of the character icon, and the left will be a listview of character stats.
Next on the stack will be another relative layout of two views side by side.
The element on the left will be a list view of class special stat modifiers and the left element will be a list view of class specific weapons. Last on the stack will be two button views in a relative layout.


Interactive Description:
The user can see a quick review of the character info and  press one of the buttons on the bottom to confirm or cancel deletion of the character.











Screen Name: Monster Start Screen

Description:
This screen shows the monster that are selected to be available in the battles, and all those they are available. The button are used to go back to the main screen and save changes.

UX Description: 
The screen will be built on a content page with a stack layout. The top of the stack will be a label view of the screen name. Next on the stack will be a scrollview. In the scrollview there is a monster name checkbox and view buttons. The bottom of the stack will be a button view for save list, add monster, and going back.

Interactive Description:
The user will click on the checkbox to add a monster. When unchecking a box, the user will be directed to the monster delete page.
The back button will return the user to the game menu.The save button will keep changes and return to the game page, not the battle sequence. The view button will send the user to the monster read screen. The add button will end the user to the monster create page











Screen Name: Monster Read

Description:
This screen shows the icon, description, traits, attacks, and weaknesses of the selected monsters.

UX Description:
The screen will be built on a content page. The screen will use a stack layout. The top of the stack will be a label view of the character name.Next will be two elements side by side in relative layout. The Image at the right will be an image view of the character icon, and the left will be a listview of character stats.
Next on the stack will be another relative layout of two views side by side.
The element on the left will be a list view of class special stat modifiers and the left element will be a list view of class specific weapons.
Bottom will be three button views in relative layout.

Interactive Description:
The user will be able to read details of the monster. The back button will return user to the monster start screen. The edit button will take the user to the edit monster screen. The delete will take the user to delete monster screen.





Screen Name: Monster Update

Description: 
This screen allows the user to change the difficulty of selected monsters. It also shows the name, icon, traits, weapon, and armor of the monsters.

UX Description:
This screen will be built on a content page with a stack layout.The first thing on the stack will be a label vie of the screen name. Next will be a label view of the monster name.Third will be an image view of the monster icon. Fourth will be a text view of the difficulty selection text. Fifth will be a list view of the difficulty options.Sixth will be relative view of two list views.First list will be monster traits, and the second list view will be monster weapons and armor.Last on the stack will be a relative view of two button views side by side.

Interactive Description:
The user will see short details on the monster. Pressing the radio buttons will change the difficulty of fighting the monster. The save button will save changes and return to the monster read screen. Add monster will save changes and add to the monsters in the game, and return to the monster start screen. The monster difficulty can be different for each monster. The choice will be given to the user as an option.







Screen Name :Monster Delete

Description:
This screen is the confirmation page to remove a monster from the available battle monsters. Buttons on the button cancel or confirm the choice to remove the monster.

UX Description:
The screen will be built on a content page. The screen will use a stack layout. The top of the stack will be a label view of the character name.Next will be two elements side by side in relative layout. The Image at the right will be an image view of the character icon, and the left will be a listview of character stats. Next on the stack will be another relative layout of two views side by side.The element on the left will be a list view of class special stat modifiers and the left element will be a listview of attacks and weaknesses.
The button of the stack will be a relative layout of two button views



interactive Description:
The user will see details about the monster. No button will cancel the deletion and return to the monster start screen. The yes button will remove the monster from showing up in battle in the game.









Screen Name: Create Monster

Description: 
The user will be able to give the monster a name, description, difficulty, icon,and average level.

UX Description:
This is a content page with a stack layout. Top of stack is a label view with screen name. Next is a textfield, followed by another textfield. Then there is another label view. Difficulty will be displayed in a list view. A scroll view will display image views of possible icons.
Second to last is a relative view with a label view and a textfield.
Lastly there is a relative layout with two button views.

Interactive description:
The user can enter a monster name and description. They can choose the difficulty by entering an integer value. The two buttons will either cancel or save/add monster.















Screen Name: Items-Start

Description:
This screen show the available type of items used in the game.

UX Description: 
The screen will be built on a content page with a stack layout. The top of the stack will be a label view of the screen name. Next on the stack will be a scrollview. In the scrollview will be a relative view of the item name and a button view. The bottom of the stack will be a relative view of two button views.

interactive Description:

The user will see the types of items available. The user can press the view button to read details on the items
The back button will return the user to the game screen.





















Screen Name: Item Read

Screen Description: This screen is the item read page. This will show the name, icon, description, location and stat modifier for each item.

UX Description:
This will be content page with a stack layout. The top of the stack will be a label view. Next will be an imageview. After the image will be three label views. Last on the stack will be a relative view of three button views.

Interactive description:
The user will be able to read descriptions of the items. The user can press the back button to return to the items start page, press the edit button to go to the item edit page, and press the delete button to go to the item delete page.


Screen Name: Item Edit

Screen Description: This page will boot up with the current saved details of the item. It will provide fields to change the details of the item. 

Ux Description: This will be a content page with a stack layout. The top of the stack will be a label view. Next will be two relative views of a label view and textfield. Following will be two relative views with a label view and scrollview.
Next will be a relative view with a labelview and a textfield.
After will be a relative view of a label view and three image views.
The bottom of the stack will be a relative view with two button views.

Interactive Description: The user will be able to write in a new name and description. They can scroll and select a new location and a new stat. Choosing a new location will change the icon images on the bottom. They can enter a new stat value and select a new icon.  The two buttons will allow to cancel the edit or save the changes.

Screen Name: Item Create

Screen Description:
This screen allows the user to create  new item. 

Ux Description: This will be a content page with a stack layout. The top of the stack will be a label view. Next will be two relative views of a label view and textfield. Following will be two relative views with a label view and scrollview.
Next will be a relative view with a labelview and a textfield.
After will be a relative view of a label view and three image views.
The bottom of the stack will be a relative view with two button views.

Interactive Description: The user will be able to write in a new name and description. They can scroll and select a new location and a new stat. Choosing a new location will change the icon images on the bottom. They can enter a new stat value and select a new icon.  The two buttons will allow to cancel creation or save the item.



Screen Name: Item Delete

Screen Description: THis screen is the itm read page. This will show the name, icon, description, location and stat modifier for  each item.

UX Description:
This will be content page with a stack layout. The top of the stack will be a label view. Next will be an imageview. After the image will be three label views. Last on the stack will be a relative view of two button views.

Interactive description:
The user will be able to read descriptions of the items. The user can press the no button to return to the items read page without deleting the item, or press the yes button to delete the item.



Screen Name: Character Party Screen

Description:

This screen shows the characters in the party. It is the first screen before a battle starts. It allows users to add and remove characters from the party. Selected characters will have its stats displayed on the bottom. 

UX Description:
This screen will be built on a content page with a stack layout. The top of the stack will be a label view. Next will be a relative layout of of two label views side by side. Underneath that will be a listview of the active party and a scroll view of available parties in a relative layout. The bottom will be relative layout of two button views. 


Interactive Description:
The user will be able to see characters in the party, and see available characters. They can press the remove button to remove a character from the party and the add button to add characters to the party.The back button will take the character to the game page. The continue button will take the user to he monster battle screen. All characters alive characters will be available in the scrollview. Will not continue if party incomplete.






Screen Name: Monster Battle Screen

Description: This screen allows the user to choose monsters to fight or let the computer decide.

UX Description: 
This will be a content page of stack layout. The top will be a label view.Next a relative view of two label views. Third will be a relative view of a listview and a scrollview.
The bottom will be a relative view of three button views.

Interactive description: 
The user can press the remove buttons to remove a monster, the add buttons to add a monster. The Back button takes the user back to the character party screen. The random button chooses monsters and updates the page. The continue button takes the user to the item select screen. Will not continue if party incomplete. 

Screen Name: Item Select Screen

Description:

This screen shows the items included in the game. IT also shows the items that are available to include in the battle.

UX Description:
This screen will be built on a content page with a stack layout. The top of the stack will be a label view. Next will be a relative layout of of two label views side by side. Underneath that will be a listview of the active items and a scroll view of available items in a relative layout. The bottom will be relative layout of two button views. 


Interactive Description:
The user will be able to see items in the party, and see available items. They can press the remove button to remove a item from the battle and the add button to add item to the battle.The back button will take the character to the character party page. The continue button will take the user to the battle screen. All items will be available in the available items scroll view







Screen Name: Battle Screen

Description:
This is where the game battles take place. It show the grid of the monster and player positions. It has lvl and hit points of the monsters and characters. The icons directly above and below the grid indicate which monster or character are involved on the next attack sequence. The dice roll for the character and monster are displayed for defense and attack. Text on the bottom show the moves, attacks, hits, misses, experience earned, characters levelled up, and the end of the round. Squares around character icons indicated characters who have completed their attacks. Circles around the character indicate who is attacking next. Button on the button allow for autoplay, rolling for attacks, and end of turn. Once the round is over selecting end turn will link to the beginning of the battle sequence.

Ux Description: 
This page will be built on a content page with a stacked layout. The top of the stack will be grid view of monster icons image view and monster levels and hit points as label views.Second on the stack is a relative view of an image view, and a label view. Next on the stack will be a grid view of monster icons and character icons as image views. Following is a relative view of a labelview and image view. Fifth on the stack will be a grid view of character icons as image views and character hit points and level as label views. Next on the stack will be a scroll view of label views. The bottom of the stack will be relative layout of three button views.

Interactive Description:
The user can see the level and hitpoint of each monster and character.They can select his character, choose a place on the map to move his character and select a monster in range to attack. User selects a character and selects an empty space to move the character, and the page is refreshed with the new location.The text below will update with hits or misses, and damage dealt.The autoplay will complete the round, automating the attacks and moves for both the computer and user. The roll button will start the attack roll for characters to monsters. Attack from the monsters wo characters will be automated, and results displayed. The characters and monsters involved on the attack or defence will be updated above and below the grid. Turns will be based on calculated initiative using character and monster speed. The text box will instruct user when it is their turn when necessary.




Screen Name: Inventory

Description:
This is the item pool screen. Users can check the items held by individual characters. THey can equip items in character inventory, or equip items from the pool of items.

UX Description:
The screen will be made of a content page using a stack layout. The top of the stack will be a grid view of character icon image views. Next on the stack will be a text view with “character inventory” written.
Third on the stack will be a scrollview of a data template. On the template will be data on the character location, item names, and a checkbox.
 Next on the stack will be a relative layout of two elements. The element on the left will a label view of character data. The element to the right will be a button view.
Next on the stack will be a label view.
Following will be will be a scrollview of items in the pool. The bottom of the stack will be button views in a relative layout.

Interactive Description:
The user can select the different character icons change its items. Selecting the boxes to the right of the players inventory are the items that will be dropped when unequip is pressed. When items are unequipped the stats will update. Check boxes to the right of items in the pool indicate which items are desired to be equipped. They will be equipped when the equip button is pressed. The weapons will not equip if multiple items of the same location are selected to be equiped. The next round button will start the next battle round, and link the user to the 

Screen Name: Battle Round Screen

Description: 
This screen display the results after a battle round.

UX description:
This will be a content page with a stack layout. THe top of the stack will be a label view, then a list view of results. Next a label view followed by a list view. Another label view and a listview. The bottom is a button view.

Interactive Description:

The user can read round stats, see which characters have died and survived. The button on the bottom takes the user to either the items pool or the game over screen















Screen Name: Game Over

Description:  This is the game over screen it shows the full battle results.

UX Description:

This will be a content page with a stack layout. The top will be a label view, followed by a list view of stats.
Next is a label view and a scrollview of characters that have died and the round they died.
Last is a button view that jumps to the high score page.

Interactive Description:

The user can read details and scroll through the characters that have died.
The button will take the user to the score screen.




Screen Name: About
Description:
This page show the app name, team name, and names of contributors.

UX Description:
The page will be built on a content page with a stack layout.
The top of the stack will be a label view of the app name. Next on the stack will be a label view of the team name. Third on the stack will be a label view of the team member names. Fourth on the stack will be a label view of the date.
Last on the stack is a button view.


Interactive Display:
The user can read details about the publishing of the app. The back button will return the user to the game screen.







