EventStoreWritersAndReaders
===========================

A project exploring the use of GetEventStore with a sprinkling of DDD/CQRS copied from [TomLiversidge's similar project](https://github.com/tomliversidge/eventstore-soccerexample)

The intention was to explore EventStore and not the DDD aggregate stuff but that was a nice side-effect.

This also includes this [TopShelf mediated EventStore Service project](https://github.com/pauldambra/EventStoreService) as a submodule to support running this project.

#What is it?!
There are two commandline applications

### 1. EventWriter
Which displays a menu allowing the user to choose quotations to include in a speech. Starting the program counts as starting a speech and quitting as finishing a speech.

### 2. QuoteSubscriber
Which connects to event store and subscribes to QuotationUsed events. Displaying the quote on screen.

This depends on a 'quotations' project

`````JavaScript
fromCategory('speech')
  .foreachStream()
  .when({
    "QuotationUsed": function(state, ev) {
       linkTo('quotations', ev );
    }
  });
`````

but could probably be supported using the built in eventType projection.

#To-do

* Programmatically create/enable the projection if it does not exist
