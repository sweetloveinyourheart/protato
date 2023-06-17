const express = require('express');
const compression = require('compression');
const path = require('path');

const app = express();
const port = 3000; // Set the desired port number

app.use(compression()); // Enable Brotli compression

// Serve static files from the 'build' directory
app.use(express.static(path.join(__dirname, 'dist')));

// Define a route to serve the WebGL files
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, 'dist', 'index.html'));
});

// Start the server
app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});
