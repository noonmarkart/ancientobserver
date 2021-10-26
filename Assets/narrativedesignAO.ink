VAR BLESSING = false
VAR EMBER = false
VAR BURIED = false
VAR Bored = false
VAR Explain = false
VAR GHOSTTIME = false

//declaring a function to trigger on the unity side
EXTERNAL gameEvent(eventName)

In a deep forest, a young fox comes across a long-forgotten temple...

-> atrium
==atrium==
1900 - The sounds of birds come through the broken, mossy walls.
* [Look Around] -> atrium.look
* [Search the moss pile] -> atrium.mossypile
* {not ghost} [Approach the door] -> atrium.ghost
+ {ghost} [Approach the door] -> atrium.ghosttalk2
+ {ghostexplainfinish} [Go into the past] -> past1
+ {not BLESSING} [Go to the Future] -> futurelocked
+ { BLESSING} [Go into Future] -> future1
+ {not EMBER} [Main door] -> doorlocked
+ {EMBER} [Main door] -> Sanctum


=look 
This structure has been crumbling for quite some time. Sections of the walls have begun to fall in. Mostly structurally sound though, just a lot of moss. Ahead is a large door, columns surround you. ->atrium 
//
//
//

=ghost 
A ghostly shade appears by the door in the shape of a human. An old human. "Hello." 
* [You are a fox. Do not respond.] -> atrium.ghosttalk
* [Tilt your head.] -> atrium.ghosttalk

=ghosttalk
"Don't be scared. I just wanted to say hello." It pauses. "The door has crumbled, hasn't it? That's how you got in." 

 *You look back.
 
 - "That's what I thought." 
 * [This is boring. Start to wander away.]
 * [Sniff the ghost.]
 - "Hey! I'm talking to you." 
 
 You stand still and sneeze, blinking up at the ghost. 
 
 - "...alright. I shan't expect much, you are just a little forest creature after all. Would you care to hear about this temple? 
 * [Yes] -> atrium.ghostexplain
 *[No] -> atrium.ghosttired
 
//

-> atrium
=ghosttired 
 ~ Bored = true 
 "Alright then." 
 -> atrium
 
 
 //
 //
 
=ghostexplainbored
"Are you going to listen to my very interesting story?"
* [Yes] -> atrium.ghostexplain
 * [No] -> atrium 
-> atrium


=ghostexplain2
"Would you like to hear the temple's history again?"
 + [Yes] -> atrium.ghostexplainrepeat
 + [No] -> atrium 
-> atrium

//
//
//
=ghostexplain
((Cutscene??)) "Not very long ago, though perhaps quite  long ago for a creature like you, this ruin was home to a brotherhood and sisterhood who worshipped the fire that they believed created all sentient life 
*[Next] 

-...The first beings were made of clay, sculpted from the soft earth of the southern continents. In the harsh temperatures of the south they melted, so their people journeyed north. Here, though, they found cruel icy winds that would freeze them solid....
*[Next] 
-...It was then that they discovered fire, only able to strike flint now with their strong, frozen solid hands...
*[Next] 
-...they soon learned that the fire not only warmed their joints, but hardened their skins. With this newfound strength they formed true families, communities. Some wished to learn more of the fires that gave them form...
*[Next] 
-...They soon came to learn that the crucible of their existence was too, alive. In gratitude, the followers cultivated a hearth, and let the fire grow. As it grew and learned about its surroundings, it began to create its own children...
*[Next] 
-...this first flame was called Aerismeus by the creatures who recognized it. It had existed since the dawn of time, given shape through the thoughts and cultivation of many creatures throughout the centuries...
*[Next] 
-...Soon all who had risen from the clays of the south were hardened by Aerismeus' flame. They were worshipped by these people, shaping eachother..It is said that Aerismeus takes the form of its worshippers, of its cultivators...
*[Next] 
-..."the many animals and people you might see around the forests are all a result of Aerismeus' spark." The ghost hums. "This temple was built to preserve the embers of the first fire of Aerismeus. Though, no one has seen Them in quite some time."
*[Next] 
-"The inner Sanctum of this temple is where Aerismeus' hearth has been kept safe all these years. It is sealed with sacred magics that only the worshippers long-gone from this temple know." 
* {not GHOSTTIME} [Next] -> atrium.ghostexplainfinish
* {GHOSTTIME} [Next] -> atrium.ghostexplainrepeat

=ghostexplainrepeat
 ((Cutscene??)) "Not very long ago, though perhaps quite  long ago for a creature like you, this ruin was home to a brotherhood and sisterhood who worshipped the fire that they believed created all sentient life 
+[Next] 

-...The first beings were made of clay, sculpted from the soft earth of the southern continents. In the harsh temperatures of the south they melted, so their people journeyed north. Here, though, they found cruel icy winds that would freeze them solid....
+[Next] 
-...It was then that they discovered fire, only able to strike flint now with their strong, frozen solid hands...
+[Next] 
-...they soon learned that the fire not only warmed their joints, but hardened their skins. With this newfound strength they formed true families, communities. Some wished to learn more of the fires that gave them form...
+[Next] 
-...They soon came to learn that the crucible of their existence was too, alive. In gratitude, the followers cultivated a hearth, and let the fire grow. As it grew and learned about its surroundings, it began to create its own children...
+[Next] 
-...this first flame was called Aerismeus by the creatures who recognized it. It had existed since the dawn of time, given shape through the thoughts and cultivation of many creatures throughout the centuries...
+[Next] 
-...Soon all who had risen from the clays of the south were hardened by Aerismeus' flame. They were worshipped by these people, shaping eachother..It is said that Aerismeus takes the form of its worshippers, of its cultivators...
+[Next] 
-..."the many animals and people you might see around the forests are all a result of Aerismeus' spark." The ghost hums. "This temple was built to preserve the embers of the first fire of Aerismeus. Though, no one has seen Them in quite some time."
+[Next] 
-"The inner Sanctum of this temple is where Aerismeus' hearth has been kept safe all these years. It is sealed with sacred magics that only the worshippers long-gone from this temple know." 
+[Next] 
-"I fear Aerismeus is growing weak. Please help Them."
-> atrium

=ghostexplainfinish
-... "I fear Aerismeus is growing weak, Their hearth has not been tended to in all these years... would you find a way to help Them? I would wait and ask...someone with, well.." The ghost looks at you. You are still, in fact, a fox. "Hands. But. Aerismeus gave life to us all, we are all responsible.
*[Give the ghost a look like "why can't YOU do it".] 
-"...Don't look at me like that."
*[Keep doing it.]
-"Stop it!"
*[Stare into the very plasma of its soul.]
-"Fine. Fine! I don't have hands either. I can't touch anything. I don't even know if anything else would be able to see me. So you do it." 
*[Huff.] 
- "I would help Them if I could. Please, succeed where I cannot." 
*[Agree.] 
-You nod your head. The ghost looks comforted. "I have been worrying for some time about this..cracks have begun forming in the temple's very foundation." 
*[Look at the crumbling wall.]
- "No...no not those cracks. Cracks in time. Fire dies over time unless tended, I fear the cracks symbolize Their decline."
*[Try to conceptualize time.]
-"..you look pained. Don't think too hard about it. Just look for glowing cracks in the temple's floor, I suspect they'll lead to other points in time." 
*[You decide not to think too hard about what time is. You nod.]
- "Please...find a way to help Aerismeus." The ghost fades from view. 

->atrium 

//
//
//
=ghosttalk2
* {Bored} [Look at the ghost pointedly.]  -> atrium.ghostexplainbored
+ {not Bored} [Cock your head curiously at the ghost.] -> atrium.ghostexplain2
 

-> atrium

//
//
//

=mossypile 
You find an ACORN. Eat the acorn? 
* [Yes] -> atrium.mossYes
* [No] -> atrium.mossNo
+ [The moss smells good.] ->END


=mossNo
It survives your snuffling ... for now. -> END

=mossYes
It tastes old. Crunchy. Your ravenous hunger is sated. -> END



==past1== 
THE PAST

1750 - The temple glistens around you. 
*[Look around] -> past1.look
+[Listen to their conversation] -> past1.P_Monk1
+[Go further into past] -> past2
+[Return to previous time] ->atrium

=look
 You can see a pair of humans talking to eachother. 
 
 "You must be careful Brother! These tomes are more important than any of our lives!" 
 
The other faithful appears to be struggling to carry them. "I'm sorry sir I'm just so worried!" 

 ->END
 
=P_Monk1
You walk up to the monk. 

"Agh!! What is a fox doing in here?! I thought you closed the chambers!"

Other human: "I did! You must not have. Adrian I must insist you take more care with your duties."
->END

=P_Monk2
You sniff at the other human. He looks at you. "Do not distract my pupil." 

->END


==past2==
PAST2 - The sounds of work fill the air. 
+[Look at the workers] -> observeNPCsP2
+[Walk north to Temple Sanctum] ->SanctumP2
+[Return to previous time] ->past1
+[Go further into the past] -> past3

==SanctumP2==
You see a lone monk in grander robes. 
+[Walk up to Monk] -> monkpast2sanctum
+[Go back to p2 atrium] ->past2
==monkpast2sanctum==
"wow sure are a fox, we like those around here. here's my blessing" 
~BLESSING = true 
->END


==observeNPCsP2==
Workers make idle chatter as they work. They all have different reactions to fox.
->END



==past3==
DISTANT PAST, 1350 - Foresty forest.
+ [You see a cool gemstone] ->rockbury
+ [Oh wow look another fox] -> foxp3
+[Return to previous time] ->past2

==rockbury==
You bury the raw gem in a place you'll remember. Maybe someday you'll see it again
~BURIED = true 
->END

==foxp3==
"Oh hi there bro"
->END

==doorlocked==

The door is cold and forboding. Solid. ->END
==futurelocked==
You are not yet attuned to the energy of this place, don't worry about it, you're a fox. Explore the past. ->END

==future1==
FUTURE, 1970 - Sick tunes blast out into the chill night. 
+ [Sniff the bottles] -> bottlesf1
+[Stargaze] -> stargazef1
+[Listen to the spraypainter] ->spraypaintf1
+[Go into Future] -> future2
+[Return to the past] ->atrium

==bottlesf1==
Tastes bad, like sour plants. Alcohol. 
->END

==stargazef1==
you look at the stars with him. 
->END

==spraypaintf1
sick taggin bro. 
->END


==future2==
FUTURE 2, 2000 -  
+[Find Shelter] ->SanctumF2
+[Return to previous time] ->future1

==SanctumF2==
You see a small, flickering hearth up ahead in the gloom. 
+[Get warm by the hearth] -> firedeity
+[Leave the shelter] ->future2
==firedeity==
"wow sure are a fox, here's my ember itll keep me warm, dont let me die thank you" 
~EMBER= true 
->END

////////////
==Sanctum==
Fire fire fire fire fire 
+ [Bring ember to fire] ->flametime
+ [Explore] -> exploreSanctum

==flametime==
"THANKS B" (rousing speech/dialogue about the passage of time and uncomfortable shit like that, ends with determination to find new home for deity. 
+[Help] -> flametime.helpfox
+[stay] -> flametime.staywithfox

=helpfox
hopeful end with fox 
-> END

=staywithfox 
Melancholy, sweet end with music
->END

==exploreSanctum==
Lots of mosaics about LIFE and CREATION and cool shit like FIRE CREATURES of all sorts. 
    ->END
