# REPR Design Pattern
https://deviq.com/design-patterns/repr-design-pattern

The REPR Design Pattern defines web API endpoints as having three components: a Request, an Endpoint, and a Response. It simplifies the frequently-used MVC pattern and is more focused on API development.

Using this approach, your API is designed around individual endpoint classes. Each one has a single Handle method that acts just like a single Controller action (because it is, under the covers). Each endpoint can define an optional Request type and an optional Response type. All together, you define endpoints using just these types:
- Request
- EndPoint
- Response

**R**equest-**E**nd**P**oint-**R**esponse, or REPR ("reaper") is a much simpler pattern for developing API endpoints than MVC. There's no View. There's no bloated controller. The only models you care about are the Request and the Response.

What about the big M model from MVC, the one with all the business logic? This pattern doesn't dictate how you implement the logic within the endpoint. You could just put all the logic in the Handle method. But for non-trivial applications you probably want to inject some service(s) into the endpoint, and minimize the amount of non-UI logic that exists in it. But whether you choose to do that or not is not a part of the REPR pattern.
