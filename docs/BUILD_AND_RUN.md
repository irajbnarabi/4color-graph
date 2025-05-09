# 🛠️ Build & Run Guide – Four-Color Graph Visualizer

This guide helps you run the full-stack application using Docker Compose, with support for environment configuration, health checks, and one-command startup.

---

## 📦 Prerequisites

Make sure you have the following installed:

| Tool           | Version    |
|----------------|------------|
| Docker         | 20+        |
| Docker Compose | v2         |
| Bash Shell     | Any modern Bash (Linux/macOS or Git Bash for Windows) |

---

## 🚀 Quick Start (Recommended)

Use the provided script to build and launch everything in one go.

```bash
chmod +x build.sh
./build.sh
```

To rebuild everything from scratch:

```bash
./build.sh --clean
```

The script will:
- Clean up old containers (if `--clean` is passed)
- Build backend and frontend images
- Start containers in the background
- Wait for backend and frontend to be live
- Print the app URL when ready

---

## 🧱 Project Structure

```
project-root/
│
├── build.sh
├── docker-compose.yml
├── .env
│
├── backend/
│   ├── Dockerfile
│   └── src/... (.NET backend files)
│
└── frontend/
    ├── Dockerfile
    └── src/... (React frontend files)
```

---

## 🌐 Application URLs

Once running:

| Service  | Default URL           |
|----------|------------------------|
| Frontend | http://localhost:3000  |
| Backend  | http://localhost:5000  |

---

## ⚙️ Environment Configuration

### `.env` in root

You can change the backend port here:

```env
BACKEND_PORT=5000
```

### `.env` in frontend

Frontend uses:

```env
REACT_APP_API_URL=http://backend:${BACKEND_PORT}
```

> Docker Compose injects this value during build.  
> React will call backend using the internal Docker network name `backend`.

---

## 🐳 Manual Docker Commands

If you don’t use `build.sh`, you can run manually:

```bash
docker-compose build
docker-compose up
```

To stop and remove everything:

```bash
docker-compose down --volumes
```

---

## 🔍 Health Check Logic (from build.sh)

The script includes automatic health checks like:

```bash
until curl -s http://localhost:5000 >/dev/null; do
  echo "⏳ Waiting for backend..."
  sleep 1
done
```

It also waits for the frontend (`http://localhost:3000`) to be reachable.

---

## 💡 Troubleshooting

| Issue                           | Solution                                                  |
|----------------------------------|------------------------------------------------------------|
| `build path ... does not exist` | Make sure you're running from the project root            |
| `dotnet restore` fails          | Check the csproj path in backend/Dockerfile               |
| React can't reach backend       | Check `REACT_APP_API_URL` value and `.env` consistency    |
| Port already in use             | Change `BACKEND_PORT` in `.env` and rebuild               |

---

## ✅ After Success

Visit the app in your browser:

[http://localhost:3000](http://localhost:3000)

---

Enjoy visualizing graphs! 🎨