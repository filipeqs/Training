var message: string = "Hello World";
console.log(message);

class Greetings {
  name: string;
  constructor(name: string) {
    this.name = name;
  }

  greet(): void {
    console.log(`Hello, I am ${this.name}`);
  }
}

var filipe = new Greetings("Filipe");
filipe.greet();
