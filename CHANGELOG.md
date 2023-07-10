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

