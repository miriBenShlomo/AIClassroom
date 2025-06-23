import React, { useEffect, useState } from 'react';
import apiService from '../api/apiService';
import {
  Box,
  Card,
  CardContent,
  Typography,
  CircularProgress,
  Alert,
} from '@mui/material';

export default function HistoryList({ userId }) {
  const [history, setHistory] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchHistory = async () => {
      try {
        const data = await apiService.getLearningHistory(userId);
        setHistory(data);
      } catch (err) {
        setError('Failed to fetch history');
      } finally {
        setLoading(false);
      }
    };

    fetchHistory();
  }, [userId]);

  if (loading) return <CircularProgress />;
  if (error) return <Alert severity="error">{error}</Alert>;

  return (
    <Box mt={4}>
      <Typography variant="h5" gutterBottom>
        Learning History
      </Typography>
      {history.length === 0 ? (
        <Typography>No history available.</Typography>
      ) : (
        history.map((item, index) => (
          <Card key={index} sx={{ mb: 2 }}>
            <CardContent>
              <Typography variant="h6">{item.prompt}</Typography>
              <Typography variant="body2">{item.response}</Typography>
              <Typography variant="caption" display="block">
                Date: {new Date(item.createdAt).toLocaleString()}
              </Typography>
            </CardContent>
          </Card>
        ))
      )}
    </Box>
  );
}
