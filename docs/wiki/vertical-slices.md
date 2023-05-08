# Vertical Slices Architecture
https://jimmybogard.com/vertical-slice-architecture/

## Introduction

A traditional layered/onion/clean architecture is monolithic in its approach:

![layered architecture](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)

The problem with this approach/architecture is that this is only really appropriate in a minority of the typical requests in a system. Additionally, these architectures tend to be mock-heavy, with rigid rules around dependency management. In practice, these rules are rarely useful, and you start to get many abstractions around concepts that really shouldn't be abstracted.

Instead, for this journey application, which need to be more flexible, a tailored approach would be more suitable, where each request are treated as a distinct use case in how to approach its code. This is called a "Vertical Slice Architecture".

So what is a "Vertical Slice Architecture"? In this style, the architecture is built around distinct requests, encapsulating and grouping all concerns from front-end to back. You take a normal "n-tier" or hexagonal/whatever architecture and remove the gates and barriers across those layers, and couple along the axis of change:

![vertical architecture](https://jimmybogardsblog.blob.core.windows.net/jimmybogardsblog/3/2018/Picture0030.png)

With this approach, most abstractions melt away, and there are no need of something like a "shared" layer abstractions like repositories, services, controllers. Sometimes these are still required by our tools (like controllers or ORM units-of-work) but we keep our cross-slice logic sharing to a minimum.
