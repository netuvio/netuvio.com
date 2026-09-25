<script setup lang="ts">
import { createError } from '#app';

throw createError({
    statusCode: 404,
    statusMessage: 'Page Not Found',
    fatal: true
});
</script>
