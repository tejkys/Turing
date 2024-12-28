using Turing;

var machine0 = new TuringMachine(TuringMachine.LoadRulesFromFile("sub.txt"));
machine0.Test("q1", "11111B111B");

var machine1 = new TuringMachine(TuringMachine.LoadRulesFromFile("add.txt"));
machine1.Test("q1", "111B11");

var machine2 = new TuringMachine(TuringMachine.LoadRulesFromFile("AplusBplusCplus.txt"));
machine2.Test("q1", "aabbcc");

var machine3 = new TuringMachine(TuringMachine.LoadRulesFromFile("AtimesBtimesCtimes.txt"));
machine3.Test("q1", "bcc");