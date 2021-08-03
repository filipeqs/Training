const message: string = 'Hello World';
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

var filipe = new Greetings('Filipe');
filipe.greet();

// Type Assertion
var str = '1';
var str2: number = <number>(<any>str);
console.log(typeof str2);

// Scopes
var globalScopes = 10;
class Numbers {
    insideClass = 11;
    static staticInsideClass = 12;

    storeNum(): void {
        var insideMethod = 13;
        console.log(insideMethod);
    }
}

console.log(globalScopes);
console.log(Numbers.staticInsideClass);
var objNumbers = new Numbers();
console.log(objNumbers.insideClass);
console.log(objNumbers.storeNum());

// Optional Parameters
function displayDetails(id: number, name: string, email?: string): void {
    console.log(`ID: ${id}`);
    console.log(`NAME: ${name}`);

    if (email != null) console.log(`EMAIL: ${email}`);
}

displayDetails(123, 'John');
displayDetails(111, 'mary', 'mary@xyz.com');

// Rest Paramenter
function addNumbers(...numbers: number[]) {
    let sum: number = 0;
    for (let index = 0; index < numbers.length; index++) {
        sum += numbers[index];
    }

    console.log(`The sum is ${sum}`);
}

addNumbers(1, 2, 3);
addNumbers(10, 10, 10, 10, 10);

// Default Parameter
function calculateDiscount(price: number, rate: number = 0.5) {
    const discount = price * rate;
    console.log(`Discount Amount: ${discount}`);
}

calculateDiscount(1000);
calculateDiscount(1000, 0.3);

// Overload
function display(s1: string): void;
function display(n1: number, s1: string): void;

function display(x: any, y?: any): void {
    console.log(x);
    console.log(y);
}

display('abc');
display(1, 'xyz');

// Array
var nums: number[] = [1, 2, 3, 4];

// Array Destructuring
var arrDestruct: number[] = [12, 13];
var [x, y] = arrDestruct;
console.log(x);
console.log(y);

// Union Type Variable
var unionVal: string | number;
unionVal = 12;
console.log('numeric value of val ' + unionVal);
unionVal = 'This is a string';
console.log('string value of val ' + unionVal);

// Union Type and function parameter
function unionDispla(name: string | string[]) {
    if (typeof name == 'string') {
        console.log(name);
    } else {
        var i;

        for (i = 0; i < name.length; i++) {
            console.log(name[i]);
        }
    }
}
unionDispla('mark');
console.log('Printing names array....');
unionDispla(['Mark', 'Tom', 'Mary', 'John']);

// Interface and Objects
interface IPerson {
    firstName: string;
    lastName: string;
    sayHi: () => string;
}

var customer: IPerson = {
    firstName: 'Tom',
    lastName: 'Hanks',
    sayHi: (): string => {
        return 'Hi there';
    },
};
console.log('Customer Object ');
console.log(customer.firstName);
console.log(customer.lastName);
console.log(customer.sayHi());

interface IMusician extends IPerson {
    instrument: string;
}

var drumer: IMusician = {
    firstName: 'Tom',
    lastName: 'Hanks',
    instrument: 'Drums',
    sayHi: (): string => {
        return 'Hi there';
    },
};

//  Class Inheritance
class Shape {
    Area: number;

    constructor(a: number) {
        this.Area = a;
    }
}

class Circle extends Shape {
    constructor(a: number) {
        super(a);
    }

    disp(): void {
        console.log('Area of the circle:  ' + this.Area);
    }
}

var obj = new Circle(223);
obj.disp();

// Class inheritance and Method Overriding
class Printer {
    doPrint(): void {
        console.log('Printing from printer!');
    }
}

class StringPrinter extends Printer {
    doPrint(): void {
        console.log('Printing from String Printer');
    }
}

var printer = new StringPrinter();
printer.doPrint();

// The instanceof operator
var stringPrinter = new StringPrinter();
var isPrinter: boolean = stringPrinter instanceof StringPrinter;
console.log(`obj is an instance of StringPrinter: ${isPrinter}`);
