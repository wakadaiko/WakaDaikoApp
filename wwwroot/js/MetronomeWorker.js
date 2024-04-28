self.addEventListener('message',
    (event) => {
        console.log("Inside the worker");
        setInterval(() => { self.postMessage(event.data + 1); }, 2000);
    }
);