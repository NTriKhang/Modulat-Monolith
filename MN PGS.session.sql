SELECT table_schema, table_name
FROM information_schema.tables
WHERE table_schema = 'events' AND table_name = 'Events';
