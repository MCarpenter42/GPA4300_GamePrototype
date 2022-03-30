||==============||==============================||==============||
||==============||    GPA4300 GAME PROTOTYPE    ||==============||
||==============||   GROUP MEMBER README FILE   ||==============||
||==============||  Chiara / Harrison / Morgan  ||==============||
||==============||==============================||==============||


Hi there! If you're reading this, that means you're one of the
group members (listed above) for this particular GPA4300 group
project - either that, or I forgot to remove this file before we
submitted the project! Either way, welcome to this little
explanation of how the hell things are working in here.


 << AN INITIAL NOTE >>

The first this I want to say is this: PLEASE DO NOT MODIFY SCRIPTS
OTHER PEOPLE HAVE WRITTEN WITHOUT COMMUNICATING WITH THEM. Even if
the code is well-commented, the comments are unlikely to fully
explain the precise relationship between the commented part and
any other code that may be connected to it.

If you want a piece of code someone else wrote to be modified for
whatever reason, whether it's because you think it could be better
structured, or because you want to allow it to interact with your
own code in some way, or any other reason, please communicate with
whoever originally wrote it, to prevent us from having to roll
back changes wherever possible.


 << FILE STRUCTURE >>

Now for the information. You may notice that there are quite a few
different scripts and classes spread across different folders - I
do this so that functionality is broken up into associated chunks.
I would recommend that, when you are writing new code in a new
class/script, you take the same approach, putting the new script
in the appropriate existing folder, or creating a new one if
necessary.


 << THE "COREFUNCTIONALITY" SCRIPT/CLASS >>

The class named "CoreFunctionality" is a script that I have the
vast majority of other classes inherit from, whether directly or
via inheritance from a different class, and the class itself
inherits from MonoBehavious. So, what does it do, and why do I
use it?

With regards to what it does, the class holds a handful of general
purpose methods and objects, things that'd I'd want to be able to
use from anywhere else in the game. If you're asking why I do that
instead of creating a custom library for general methods, it's
simply because I haven't bothered to learn how to yet.

I recommend you take a look at it, because I personally think that
some of the methods in there are actually pretty useful, and you
might get use out of them yourself in your own scripts! I'd also
recommend that you make your classes inherit from it, as then not
only do they inherit everything they would from MonoBehaviour, but
also all the custom stuff.


 << THE INTERACTION SYSTEM >>

So, there's a scipt for handling interactions, but you don't use
it by just adding the script to the relevant GameObject. Instead,
you need to add one of these two prefabs to it as a child object:

 - InteractionHandler
 - LockedInteractionHandler
 
The standard version of this script/class is used in most cases,
and the "Locked" version is used whenever you want to have an
object require a "key" item - whether an actual key or something
else - to be interacted with, such as a door or treasure chest.

The interaction handler object consists of the object itself, as
well as an interaction icon, which is actually a UI canvas object
that exists in the world space, rather than as an overlay. When
positioning the interaction handler, you'll want to set the
position of the main object as the actual point the player
interacts with, and then set the position of the icon relative to
that so that it doesn't clip with anything too severely.

To make the interaction handler actually *do* something, you'll
want to take a look at it in the inspector. There's a field for
you to add an "Interact Event", which is what you use to connect
the script to one on a different object. Drag the object you want
the interaction to use into the box that's looking for a
GameObject, then select the method you want the interaction to
trigger from the drop-down box. Make sure to add the script with
the method you want to trigger to the right object!
