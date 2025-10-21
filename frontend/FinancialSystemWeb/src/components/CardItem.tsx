import React from "react";
import { Card, Typography, Box } from "@mui/material";
import { useNavigate } from "react-router-dom";

interface CardItemProps {
  title: string;
  icon: React.ReactNode;
  route: string;
  background?: string;
  textColor?: string;
}

const CardItem: React.FC<CardItemProps> = ({
  title,
  icon,
  route,
  background = "#fff",
  textColor = "#000",
}) => {
  const navigate = useNavigate();

  return (
    <Card
      sx={{
        width: 200,
        height: 160,
        cursor: "pointer",
        transition: "transform 0.2s",
        "&:hover": {
          transform: "scale(1.05)",
        },
        background,
        color: textColor,
        boxShadow: 3,
        borderRadius: 2,
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
      }}
      onClick={() => navigate(route)}
    >
      <Box mb={1}>{icon}</Box>
      <Typography variant="h6">{title}</Typography>
    </Card>
  );
};

export default CardItem;
