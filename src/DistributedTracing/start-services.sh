#!/bin/bash

echo "Init NotificationService..."
dotnet run --project NotificationService/ &
Service/ &
sleep 2

echo "Init PagamentoService..."
dotnet run --project PaymentService/ &
sleep 2

echo "Init OrderService (client)..."
dotnet run --project OrderService/
