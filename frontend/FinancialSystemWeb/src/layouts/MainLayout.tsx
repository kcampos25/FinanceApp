import React from "react";
import {
  AppBar,
  Toolbar,
  Typography,
  Button,
  Menu,
  MenuItem,
  Box,
  CssBaseline,
  Container,
} from "@mui/material";
import { useNavigate, useLocation } from "react-router-dom";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";

const MainLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const navigate = useNavigate();
  const location = useLocation();

  const isSelected = (path: string) => location.pathname === path;

  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const toolsMenuOpen = Boolean(anchorEl);

  const handleToolsMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleToolsMenuClose = () => {
    setAnchorEl(null);
  };

  const handleNavigate = (path: string) => {
    navigate(path);
    handleToolsMenuClose();
  };

  return (
    <Box sx={{ display: "flex", flexDirection: "column", minHeight: "100vh" }}>
      <CssBaseline />
      <AppBar position="fixed">
        <Toolbar sx={{ justifyContent: "space-between" }}>
          <Typography variant="h6" onClick={() => navigate("/")} sx={{ cursor: "pointer" }}>
            Financial System
          </Typography>

          <Box>
            <Button
              color="inherit"
              onClick={() => navigate("/")}
              sx={{ fontWeight: isSelected("/") ? "bold" : "normal" }}
            >
              Home
            </Button>

            <Button
              color="inherit"
              onClick={handleToolsMenuOpen}
              endIcon={<ArrowDropDownIcon />}
              sx={{
                fontWeight: location.pathname.startsWith("/banks") ? "bold" : "normal",
              }}
            >
              Tools
            </Button>

            <Menu anchorEl={anchorEl} open={toolsMenuOpen} onClose={handleToolsMenuClose}>
              <MenuItem onClick={() => handleNavigate("/banks")}>Banks</MenuItem>
              <MenuItem onClick={() => handleNavigate("/currencies")}>Currencies</MenuItem>
              <MenuItem onClick={() => handleNavigate("/depositCertificates")}>
                Deposit Certificates
              </MenuItem>
            </Menu>
          </Box>
        </Toolbar>
      </AppBar>

      <Toolbar />

      <Box
        component="main"
        sx={{
          flexGrow: 1,
          display: "flex",
          justifyContent: "center",
          alignItems: "flex-start",
          bgcolor: "transparent",
          px: 2,
        }}
      >
        <Container sx={{ mt: 4, mb: 4 }}>{children}</Container>
      </Box>
    </Box>
  );
};

export default MainLayout;
