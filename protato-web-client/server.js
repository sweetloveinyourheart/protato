const express = require('express');
const path = require('path');

const app = express();
const port = 3000; // Set the desired port number

// Serve static files from the 'build' directory
app.use(express.static(path.join(__dirname, 'static')));

// Define a route to serve the WebGL files
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, 'static', 'index.html'));
});

// Start the server
app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});
