import React from 'react';
import { Box } from '@mui/material';

const PageContainer: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <Box
      sx={{
        width: '100%',
        maxWidth: 900,
        bgcolor: 'background.paper',
        borderRadius: 2,
        boxShadow: 3,
        p: 4,
      }}
    >
      {children}
    </Box>
  );
};

export default PageContainer;
