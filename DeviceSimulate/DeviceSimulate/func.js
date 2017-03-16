


function main(ch01, ch02, ch03, ch04, ch13) {
    var Pabs = 101.3529;

    //step 1.         
    var NGP_scaled = ((ch01 / Pabs) - (-0.0004411)) / 0.00015573;

    //step 2.
    var NGFlow = 293.15 / (ch13 + 273.15) * 1000 * (-1.00919 + Math.sqrt(NGP_scaled));

    //step 3.
    var MVP;
    if (ch04 / 100 < 0.25)
        MVP = 0.25;
    else
        MVP = ch04 / 100;

    //step 4.
    var VFF = 4.2672 * (Math.log(MVP)) ^ 5 + 16.0123 * (Math.log(MVP)) ^ 4 + 21.5814 * (Math.log(MVP)) ^ 3 + 11.5876 * (Math.log(MVP)) ^ 2 - 0.1304 * (Math.log(MVP)) - 5.8157 - 2.6878475;

    //step 5.
    var O2Flow = 293.15 / (ch13 + 273.15) * (((ch02 / Pabs) - 0.023 / 14.7) * 1000000 / Math.exp(VFF)) ^ (1 / 1.9);

    //step 6. optional
    var FR = NGFlow * 1000 / 10 ^ 6;

    //step 7. optional
    var STOICH = O2Flow / NGFlow;

    return STOICH;
}


var result = main(6, 20, -999999, 3300, 22);
console.log('the result is:' + result);