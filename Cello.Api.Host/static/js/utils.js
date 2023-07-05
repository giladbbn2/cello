
const xhr = (method, url, requestHeaders, payload, cbSuccess, cbFail) => {
    if (typeof cbSuccess === "undefined")
        cbSuccess = function (a) { };

    if (typeof cbFail === "undefined")
        cbFail = function () { };

    if (typeof url === "undefined") {
        cbFail();

        return;
    }

    if (typeof method === "undefined" || method === null) {
        method = "GET";
    } else {
        method = method.toUpperCase();
    }

    var xhr = new XMLHttpRequest();

    xhr.open(method, url, true);

    if (typeof requestHeaders === "undefined" || requestHeaders === null) {
        xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    } else {
        for (var k in requestHeaders) {
            if (requestHeaders.hasOwnProperty(k)) {
                xhr.setRequestHeader(k, requestHeaders[k]);
            }
        }
    }

    if (typeof payload === "undefined") {
        payload = null;
    }

    //console.log(payload);

    xhr.send(payload);

    xhr.onload = function () {
        if (xhr.status != 200) {
            console.log(`xhr error ${xhr.status}: ${xhr.statusText}`);

            cbFail();
        } else {
            cbSuccess(xhr.response);
        }
    };

    xhr.onerror = function () {
        cbFail();
    };
};

const xhrPromise = (method, url, requestHeaders, payload) => {
    return new Promise((res, rej) => {
        xhr(method, url, requestHeaders, payload, (response) => {
            res(response);
        }, () => {
            rej();
        });
    });
};

