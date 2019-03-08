# SinglyLinkedListTest
This is just a thought experiment about how one could check for loops in a Singly Linked List in C# (rather than simply storing seen pointers in a hash table as you would in C or C++).
This is simply for my own curiosity and reference.

The app simply generates a Singly Linked List, but purposefully throws in some bad references to form infinite loops into the list.  It then tries to catch those loops using a HashSet.

The objects in the list are generated with a large list of integers to inflate their size in memory. This is to show that the HashSet does not copy the objects, but only references to them.

This makes it both time and memory efficient.
