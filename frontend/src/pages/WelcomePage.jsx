import React from 'react';
import { Typography, Container } from '@mui/material';

export default function WelcomePage() {
  return (
    <Container>
      <Typography variant="h4" sx={{ mt: 4 }}>
        Welcome! You’ve successfully registered.
      </Typography>
    </Container>
  );
}
