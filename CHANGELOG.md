# [v1.4.1] - 2023-12-11 [PR: #26](https://github.com/aksio-insurtech/MongoDB/pull/26)

### Fixed

- Upgrading `Aksio.Fundamentals` to the latest version.


# [v1.4.0] - 2023-12-11 [PR: #25](https://github.com/aksio-insurtech/MongoDB/pull/25)

### Added

- Added a serializer for `Type`.



# [v1.3.0] - 2023-12-11 [PR: #24](https://github.com/aksio-insurtech/MongoDB/pull/24)

### Added

- Added a serializer for `Type`.



# [v1.2.3] - 2023-12-11 [PR: #23](https://github.com/aksio-insurtech/MongoDB/pull/23)

### Fixed

- Adding the short assembly name to the discriminators for types, the format is then `{Type.Fullname}, {Assembly.Name}`. We don't want version info, just enough to get back to types.


# [v1.2.2] - 2023-12-11 [PR: #0]()

Forcing a build. We had the `Samples` project build packages, which lead to build errors since we tried publishing it. 

# [v1.2.1] - 2023-12-11 [PR: #22](https://github.com/aksio-insurtech/MongoDB/pull/22)

** IMPORTANT **
This version is a breaking change from the previous. Due to minutes between release, we're going for "its probably fine" :) 

### Fixed

- Switched from `AssemblyQualifiedName` to `FullName`.

# [v1.2.0] - 2023-12-11 [PR: #21](https://github.com/aksio-insurtech/MongoDB/pull/21)

### Added

- Added a custom discriminator convention and a class map convention that overrides all types and sets the default discriminator to the `AssemblyQualifiedName`, allowing it to actually find the correct type when deserializing, which was not the case at all for our things.

### Fixed

- Allowing all types to be serialized with a discriminator, fixing a behavioral breaking change in the MongoDB driver.


# [v1.1.2] - 2023-12-7 [PR: #20](https://github.com/aksio-insurtech/MongoDB/pull/20)

### Fixed

- Upgraded to the latest MongoDB driver


# [v1.1.1] - 2023-12-5 [PR: #0]()

No release notes

# [v1.1.0] - 2023-12-1 [PR: #18](https://github.com/aksio-insurtech/MongoDB/pull/18)

### Added

- Added a cross cutting approach to resilience by leveraging dynamic proxies that wraps write calls to Mongo collections with a retry mechanism for failures. In addition it also takes into consideration the connection pool and limits the number of actions performed based on available connections in the pool. The retry policy will run 5 attempts with 200ms delay between each attempt.



# [v1.0.19] - 2023-9-22 [PR: #17](https://github.com/aksio-insurtech/MongoDB/pull/17)

### Fixed

- Fixed `ConceptAs` serializer for values of `ulong` (`UInt64`) types. Instead of calling `Converter.ChangeType()` which actually throws an exception if the value is outside the range of what is `MinValue` or `MaxValue` of a `long` (`Int64`), we now cast it using a regular cast from `ulong` to `long` and back to `ulong` when deserializing from Bson.


# [v1.0.18] - 2023-7-18 [PR: #16](https://github.com/aksio-insurtech/MongoDB/pull/16)

### Fixed

- Upgrade Fundamentals


# [v1.0.17] - 2023-7-18 [PR: #15](https://github.com/aksio-insurtech/MongoDB/pull/15)

### Fixed

- Upgrade Fundamentals.


# [v1.0.16] - 2023-7-18 [PR: #14](https://github.com/aksio-insurtech/MongoDB/pull/14)

### Fixed

- Upgrade Fundamentals


# [v1.0.15] - 2023-7-18 [PR: #13](https://github.com/aksio-insurtech/MongoDB/pull/13)

## Summary

Summary of the PR here. The GitHub release description is created from this comment so keep it nice and descriptive.
Remember to remove sections that you don't need or use.
If it does not make sense to have a summary, you can take that out as well.

### Added

- Describe the added features

### Changed

- Describe the outwards facing code change

### Fixed

- Describe the fix and the bug

### Removed

- Describe what was removed and why

### Security

- Describe the security issue and the fix

### Deprecated

- Describe the part of the code being deprecated and why


# [v1.0.14] - 2023-7-18 [PR: #12](https://github.com/aksio-insurtech/MongoDB/pull/12)

### Fixed

- Upgraded Fundamentals


# [v1.0.13] - 2023-7-14 [PR: #11](https://github.com/aksio-insurtech/MongoDB/pull/11)

### Fixed

- Upgraded Fundamentals


# [v1.0.12] - 2023-7-11 [PR: #10](https://github.com/aksio-insurtech/MongoDB/pull/10)

### Fixed

- Upgraded Aksio Fundamentals


# [v1.0.11] - 2023-7-11 [PR: #9](https://github.com/aksio-insurtech/MongoDB/pull/9)

### Fixed

- When serializing types that has properties represented as object and the content is a `Guid`, by default it would throw an exception "GuidSerializer cannot serialize a Guid when GuidRepresentation is Unspecified.". This release adds an object serializer with the same Guid representation as other places.


# [v1.0.10] - 2023-7-10 [PR: #8](https://github.com/aksio-insurtech/MongoDB/pull/8)

### Fixed

- Removing memory leakage from logging by formalizing the `Action<>` as private methods and also not enable logging unless `Trace` log level is set.


# [v1.0.9] - 2023-6-21 [PR: #7](https://github.com/aksio-insurtech/MongoDB/pull/7)

### Fixed

- Fixing the hookup of class maps. After refactoring the calling of the generic `Register` method is now static and not an instance method.


# [v1.0.8] - 2023-6-21 [PR: #6](https://github.com/aksio-insurtech/MongoDB/pull/6)

### Fixed

- Switching to use the `ITypes` default instance for the `DefaultMongoDBArtifacts` instead of just the project referenced assemblies. This way we are guaranteed all types we want.


# [v1.0.7] - 2023-6-21 [PR: #5](https://github.com/aksio-insurtech/MongoDB/pull/5)

### Fixed

- Changing initialization of MongoDB defaults to be a static operation at startup that should only happen once. 


# [v1.0.6] - 2023-6-20 [PR: #4](https://github.com/aksio-insurtech/MongoDB/pull/4)

### Fixed

- Upgrading Fundamentals


# [v1.0.5] - 2023-6-20 [PR: #3](https://github.com/aksio-insurtech/MongoDB/pull/3)

### Fixed

- Upgraded Fundamentals


# [v1.0.4] - 2023-6-19 [PR: #2](https://github.com/aksio-insurtech/MongoDB/pull/2)

### Fixed

- Upgraded Aksio Fundmantals


# [v1.0.3] - 2023-6-16 [PR: #0]()

No release notes

# [v1.0.2] - 2023-6-14 [PR: #1](https://github.com/aksio-insurtech/MongoDB/pull/1)

### Fixed

- Formalizing how we define the MongoDB artifacts that can be discovered / used by the extension.


# [v1.0.1] - 2023-5-30 [PR: #0]()

No release notes

# [v1.0.0] - 2023-5-24 [PR: #0]()

No release notes

# [v0.0.2] - 2023-5-24 [PR: #0]()

No release notes

# [v0.0.1] - 2023-5-24 [PR: #0]()

No release notes

