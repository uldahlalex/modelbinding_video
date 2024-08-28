#!/bin/bash
dotnet ef dbcontext scaffold \
  "DataSource=db.db" \
  Microsoft.EntityFrameworkCore.Sqlite \
  --output-dir Models \
  --context-dir . \
  --context HospitalContext  \
  --no-onconfiguring \
  --data-annotations \
  --force