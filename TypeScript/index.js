var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var message = 'Hello World';
console.log(message);
var Greetings = /** @class */ (function () {
    function Greetings(name) {
        this.name = name;
    }
    Greetings.prototype.greet = function () {
        console.log("Hello, I am " + this.name);
    };
    return Greetings;
}());
var filipe = new Greetings('Filipe');
filipe.greet();
// Type Assertion
var str = '1';
var str2 = str;
console.log(typeof str2);
// Scopes
var globalScopes = 10;
var Numbers = /** @class */ (function () {
    function Numbers() {
        this.insideClass = 11;
    }
    Numbers.prototype.storeNum = function () {
        var insideMethod = 13;
        console.log(insideMethod);
    };
    Numbers.staticInsideClass = 12;
    return Numbers;
}());
console.log(globalScopes);
console.log(Numbers.staticInsideClass);
var objNumbers = new Numbers();
console.log(objNumbers.insideClass);
console.log(objNumbers.storeNum());
// Optional Parameters
function displayDetails(id, name, email) {
    console.log("ID: " + id);
    console.log("NAME: " + name);
    if (email != null)
        console.log("EMAIL: " + email);
}
displayDetails(123, 'John');
displayDetails(111, 'mary', 'mary@xyz.com');
// Rest Paramenter
function addNumbers() {
    var numbers = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        numbers[_i] = arguments[_i];
    }
    var sum = 0;
    for (var index = 0; index < numbers.length; index++) {
        sum += numbers[index];
    }
    console.log("The sum is " + sum);
}
addNumbers(1, 2, 3);
addNumbers(10, 10, 10, 10, 10);
// Default Parameter
function calculateDiscount(price, rate) {
    if (rate === void 0) { rate = 0.5; }
    var discount = price * rate;
    console.log("Discount Amount: " + discount);
}
calculateDiscount(1000);
calculateDiscount(1000, 0.3);
function display(x, y) {
    console.log(x);
    console.log(y);
}
display('abc');
display(1, 'xyz');
// Array
var nums = [1, 2, 3, 4];
// Array Destructuring
var arrDestruct = [12, 13];
var x = arrDestruct[0], y = arrDestruct[1];
console.log(x);
console.log(y);
// Union Type Variable
var unionVal;
unionVal = 12;
console.log('numeric value of val ' + unionVal);
unionVal = 'This is a string';
console.log('string value of val ' + unionVal);
// Union Type and function parameter
function unionDispla(name) {
    if (typeof name == 'string') {
        console.log(name);
    }
    else {
        var i;
        for (i = 0; i < name.length; i++) {
            console.log(name[i]);
        }
    }
}
unionDispla('mark');
console.log('Printing names array....');
unionDispla(['Mark', 'Tom', 'Mary', 'John']);
var customer = {
    firstName: 'Tom',
    lastName: 'Hanks',
    sayHi: function () {
        return 'Hi there';
    }
};
console.log('Customer Object ');
console.log(customer.firstName);
console.log(customer.lastName);
console.log(customer.sayHi());
var drumer = {
    firstName: 'Tom',
    lastName: 'Hanks',
    instrument: 'Drums',
    sayHi: function () {
        return 'Hi there';
    }
};
//  Class Inheritance
var Shape = /** @class */ (function () {
    function Shape(a) {
        this.Area = a;
    }
    return Shape;
}());
var Circle = /** @class */ (function (_super) {
    __extends(Circle, _super);
    function Circle(a) {
        return _super.call(this, a) || this;
    }
    Circle.prototype.disp = function () {
        console.log('Area of the circle:  ' + this.Area);
    };
    return Circle;
}(Shape));
var obj = new Circle(223);
obj.disp();
// Class inheritance and Method Overriding
var Printer = /** @class */ (function () {
    function Printer() {
    }
    Printer.prototype.doPrint = function () {
        console.log('Printing from printer!');
    };
    return Printer;
}());
var StringPrinter = /** @class */ (function (_super) {
    __extends(StringPrinter, _super);
    function StringPrinter() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    StringPrinter.prototype.doPrint = function () {
        console.log('Printing from String Printer');
    };
    return StringPrinter;
}(Printer));
var printer = new StringPrinter();
printer.doPrint();
// The instanceof operator
var stringPrinter = new StringPrinter();
var isPrinter = stringPrinter instanceof StringPrinter;
console.log("obj is an instance of StringPrinter: " + isPrinter);
