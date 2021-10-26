
/*
Cheat sheet

=== free_store ===
marks a "knot", which is a dialogue, monologue, or cutscene accessible through in-game interaction. They are the entry point to any dialogues.

= objection_to_meeting
marks a "stitch", which is a storylet or a passage in twine terms

* [normal option] can be choosen only once
+ [sticky option] won't diappear (useful for navigation and interaction start/end)

+ [option]
    + + [nested option]

-> divert aka links to stitch or knot

//single line comment

TODO marks something as still unwritten passage

//choice conditioned to visited passage
* {neopagans_leave} [Choice]

//if / else
{
-passage_title:
    do this
-VARIABLE_NAME >= 2:
    do that
-else:
    otherwise
}

*/

//declaring a function to trigger on the unity side
EXTERNAL gameEvent(eventName)


//I like to use allcaps for global variable to distinguish them from knots ids
VAR KEY = false
VAR LEVEL = 0


//a choice conditioned by the int variable
==warren_hall==
Warren Hall is full of demonic administrators. They seem very dangerous, do you want to fight them?
//gameplay fight placeholder
+ {LEVEL < 3} [YES] -> lose
+ {LEVEL >= 3} [YES] -> win
+ [NO] -> END

//non repeated choices - choices can be followed by content or by diverts to other passages (knot)
== paolo ==
* [Say hello] -> intro
* {intro} [Missing homework] -> excuse
* {intro} [Offer help] -> quest
+ [Leave]
You: See you later.
Paolo: Ciao.
->END

//this knot is under paolo, note the =
= intro
You: <anim=lightpos>What's up Paolo!</anim>
Paolo: <b>Nothing good</b>, <size=10>class is starting</size> and I lost the key to 307.
->paolo

//example of weak/expressive choice, both choices take you to the same spot.
= excuse
You: <anim=lightrot>I'm sorry</anim>, I couldn't do the <i>assignment</i> because my laptop ran out of battery.
Paolo: Can't you just<delay=0.5>...</delay>buy a new laptop?
* [Too expensive] 
    You: Too expensive.
    Paolo: <anim=fullshake>Dude, you are paying 1 million dollar to come to CMU.</anim>
* [Lie]
    You: <animation=slowsine>The Apple store was flooded.</anim> //choice text doesn't have to be literal
    Paolo: I see, man made climate change.
//- (weave) is for linking back after a series of choices
- Paolo: Well, it doesn't matter anyway. If we can't get into 303 class is cancelled.
->paolo

= quest
You: I really want to attend this class, is there anything I can do?
Paolo: Well you could defeat the demons of Warren Hall and get a new key. But you better level up first at the gym.
//trigger a function gameEvent with parameter
~ gameEvent("quest")

-> paolo

//for cfa the game logic is managed with ink variables
== cfa ==
{
- not KEY: 
-> locked
- else:
-> unlocked
}

//for gym the game logic is managed with game events
//activating and deactivating two interactables

//a links (diverts) can be used without player choices
== gym ==
You have no time to work out.
->END

//increasing an int variable
== level_up ==
~ gameEvent("levelUp")
~LEVEL+=1
You work out super hard until you level up. Your current level is {LEVEL}.
{
- LEVEL < 3:
You are not ready to fight the administrators of Warren Hall.
- else:
You feel strong enough.
~ gameEvent("maxed")
}
->END

== lose ==
The admin demons slaughter you.
~ gameEvent("gameOver")
->END

//moral choice with consequences
== win ==
You unsheat... unsheath... unsheathe your Tartan sword and slay the members of the board of trustees one by one. The one that is on Jeffrey Epstein's Black Book begs you for mercy.
*[Spare him] -> spare
*[Finish him] -> kill

== spare ==
You let the dude go. He earned a lesson and you are probably a good guy. Unfortunately, the key is nowhere to be found. You leave campus uneducated.
THE END.
~ gameEvent("ambiguousEnding")
->END

//changing a boolean variable
== kill ==
You decapitate the trustee, his demonic body dissolves, a golden key is dropped. It looks like the key to 303!
~KEY = true
~ gameEvent("getKey")
->END

== locked ==
You try to enter room 303 but the door is locked.
->END

== unlocked ==
The door opens! You saved todays class. Paolo is very impressed and forgives you for your missing assignment.
~ gameEvent("happyEnding")
->END

