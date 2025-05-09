#!/bin/bash

echo ""
echo "🧹 Checking for --clean flag..."
if [ "$1" == "--clean" ]; then
  echo "🔻 Stopping and removing previous containers + volumes..."
  docker-compose down --volumes
  echo "✅ Cleanup done."
fi

echo ""
echo "🔧 Building Docker containers..."
docker-compose build

echo ""
echo "🚀 Starting services..."
docker-compose up -d

echo ""
echo "🩺 Waiting for services to become healthy..."

until curl -s http://localhost:5000 >/dev/null; do
  echo "⏳ Waiting for backend on port 5000..."
  sleep 1
done
echo "✅ Backend is up!"

until curl -s http://localhost:3000 >/dev/null; do
  echo "⏳ Waiting for frontend on port 3000..."
  sleep 1
done
echo "✅ Frontend is up!"

echo ""
echo "✅ All services are healthy and running."
echo ""
echo "🌐 Open your browser and visit:"
echo -e "\e[1;32m👉 http://localhost:3000\e[0m"
echo ""
