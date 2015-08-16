# firehawk
### Firehawk
#### _Mapping by code conventions made easy_

_Mapping by code conventions..._  

NHibernate Mapping by Code Conventions provides an easy way to start with your project’s database by mapping your model to a relational database schema based on a set of conventions.

If you have previously worked with mapping by code conventions you probably know that is not a trivial task to instruct it to work according your needs. It requires a bit of understanding about how the Conventional Model Mapper works and unfortunately is really difficult to find useful documentation about it.

_... made easy_

Firehawk is a library that helps you to configure NHibernate mapping by code conventions in a fluent and comprehensible way, providing the most standard SQL naming conventions and enabling you to create custom naming conventions.

The aim of Firehawk is not to replace your mapping API, instead, it works with mapping by code conventions generating all the database object names consistently, following a given set of naming conventions and naming rules. This avoids you to hardcode names on mapping files and reduces the need to add custom mappings classes.

Additionally, Firehawk introduces a set of mapping conventions that automatically map, base entities, components, elements and relationships reducing even more the need to add custom mapping files.

##### Benefits of Firehawk

These are some of the main benefits of using Firehawk:
* Saves you startup time by providing a very intuitive configuration API that hides all the cumbersome details of mapping by code conventions.
* Adds a set of mapping conventions that automatically map Base Entities, Components, Elements, and Relationships, reducing the need to add custom mappings.
* Provides a set of well-known SQL naming conventions that fit the most standard database naming conventions.
* Prevents the hardcoding of database object names (Tables, Columns, etc) on custom mappings.
* Allows you to create custom naming rules
* Provides access to the underlying ModelMapper to fit custom needs
* Does not affect the performance of the target application.

##### The Power of Naming Conventions
Naming conventions can be applied to several domain/database objects and can be combined to get powerful results.
Next, a list of conceptual database objects whose names can be generated through the using Firehawk naming conventions:
######_Schemas*_
* SQL Server databases schemas

######_Tables_
* Entities tables
* Components tables
* Many To Many relationships tables
* Elements tables

######_Columns_
* Entities columns
* Primary Key columns
* Foreign Key columns
* One-to-one components columns

######_Constraints_
* Primary Key constraints*
* Foreign Key constraints

_*This feature is available only through the Firehawk.MsSql extension package. See the [Installing Firehawk.MsSql Extension](https://github.com/HomeroThompson/firehawk/wiki/Install-Firehawk.MsSql-Extension) page to get more detail about this package._ 

You can read more about naming conventions on the [documentation](https://github.com/HomeroThompson/firehawk/wiki/Index) page or on [this example](https://github.com/HomeroThompson/firehawk/wiki/Example).

##### Using Firehawk

This is all you need to get Firehawk working:

    var nhConfig = new NHibernate.Cfg.Configuration();
    Firehawk.Init().BuildMappings(nhConfig);

Although looks simple, the previous code does nothing. Let’s integrate Firehawk with your project.

##### The Entities
Mapping by Code Conventions needs to know what the base entity types are in order to distinguish the domain entities from other domain objects.
So we're going to use the _Entites Configuration Section_ to tell Firehawk where the entities are:

    Firehawk.Init()
      .Configure()
        .ConfigureEntities()
          .AddBaseEntity<BaseEntityA>()
          .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("MyProject.Domain"))
        .EndConfig()
      .EndConfiguration()
    .BuildMappings(nhConfig);

##### The Mapping Classes

As mentioned previously Firehawk doesn't replace the using of mapping files. Instead it introduces a set of mapping rules that automatically map Components, Elements, Relationships, etc. reducing considerably the using of mapping classes. 
When needed you can add them by using the mapping configuration section to tell Firehawk where the mapping files are:


    Firehawk.Init()
     .Configure()
      .ConfigureEntities()
       .AddBaseEntity<BaseEntityA>()
       .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("MyProject.Domain"))
       .EndConfig()
      .ConfigureMappings()
       .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
       .EndConfig()
      .EndConfiguration()
    .BuildMappings(nhConfig);

Thats all you need to get Firehawk working using the default naming conventions.
Let's add some naming conventions.

##### The Naming Conventions
Now, we're going to use the naming conventions configuration section to tell Firehawk that we want to generate the table names in _Pascal Case_ and the column names in _Camel Case_:


    Firehawk.Init()
     .Configure()
      .ConfigureEntities()
       .AddBaseEntity<BaseEntityA>()
       .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("MyProject.Domain"))
       .EndConfig()
      .ConfigureMappings()
       .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
       .EndConfig()
      .ConfigureNamingConventions()
       .UseConventionForTableNames(TablesNamingConvention.PascalCase)
       .UseConventionForColumnNames(ColumnsNamingConvention.CamelCase)
      .EndConfig()
     .EndConfiguration()
    .BuildMappings(nhConfig);

As you can see, adding or changing a naming convention is as easy as adding a line of code.

##### Ready to Start

Please, read the [Documentation](https://github.com/HomeroThompson/firehawk/wiki/Index) to get more detail about the full set of features that Firehawk offers.
