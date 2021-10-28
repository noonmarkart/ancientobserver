//declaring a function to trigger on the unity side
EXTERNAL gameEvent(eventName)
EXTERNAL returnStringEvent(eventName)

== function gameEvent(eventName) ==
~ return // placeholder result

== function returnStringEvent(eventName) ==
~ return "emptyString" // placeholder result

VAR teleportString = "emptyString"

== teleport ==
~ returnStringEvent("getTeleportText")
+ [Yes] -> enter
+ [No] -> cancel

= enter
~ gameEvent("teleport")
-> end

= cancel
You decide against it.
-> end

= end
->END