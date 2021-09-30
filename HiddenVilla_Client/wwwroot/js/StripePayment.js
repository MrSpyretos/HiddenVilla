redirectToCheckout = function (sessionId) {
    var stripe = Stripe('PublishableKey');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
}