If you have changed/created a model and you need to create a migration run

```dotnet ef migrations add {WhatYouDidAndWhatShouldBeInTheMigration}```

If the migration is created and you need to update the database.

```dotnet ef database update```